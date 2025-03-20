import React, { useState, useEffect } from "react";
import {
  Card,
  CardContent,
  Typography,
  Box,
  Grid,
  CircularProgress,
  Avatar,
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  IconButton,
  Alert,
} from "@mui/material";
import {
  LocalFlorist as PlantIcon,
  Checklist as ChecklistIcon,
  TipsAndUpdates as TipsIcon,
  CalendarMonth as ScheduleIcon,
  Build as MaterialsIcon,
  Info as InfoIcon,
  CheckCircle as CheckCircleIcon,
  Cancel as CancelIcon,
  Close as CloseIcon,
} from "@mui/icons-material";
import { styled } from "@mui/system";
import api from "@/services/api";

// --- Styled Components ---

const Container = styled(Box)(({ theme }) => ({
  minHeight: "100vh",
  padding: theme.spacing(6),
  background: "linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%)",
  [theme.breakpoints.down("md")]: {
    padding: theme.spacing(3),
  },
}));

const StyledCard = styled(Card)(({ theme }) => ({
  borderRadius: theme.spacing(2),
  boxShadow: "0 8px 24px rgba(0, 0, 0, 0.1)",
  transition: "all 0.3s ease",
  "&:hover": {
    transform: "translateY(-8px)",
    boxShadow: "0 12px 32px rgba(0, 0, 0, 0.15)",
  },
  backgroundColor: theme.palette.background.paper,
  border: `1px solid ${theme.palette.grey[200]}`,
}));

const CardHeader = styled(Box)(({ theme }) => ({
  display: "flex",
  alignItems: "center",
  padding: theme.spacing(2),
  background: `linear-gradient(to right, ${theme.palette.primary.main}, ${theme.palette.primary.light})`,
  color: theme.palette.common.white,
  borderRadius: `${theme.spacing(2)} ${theme.spacing(2)} 0 0`,
}));

const CardContentStyled = styled(CardContent)(({ theme }) => ({
  padding: theme.spacing(3),
  "&:last-child": { paddingBottom: theme.spacing(3) },
}));

const SectionTitle = styled(Typography)(({ theme }) => ({
  fontWeight: 700,
  color: theme.palette.primary.main,
  margin: theme.spacing(2, 0, 1),
}));

const StepText = styled(Typography)(({ theme }) => ({
  marginBottom: theme.spacing(1),
  marginLeft: theme.spacing(3),
  color: theme.palette.text.secondary,
  lineHeight: 1.7,
  fontSize: "1rem",
  "&::before": {
    content: '"• "',
    color: theme.palette.primary.main,
    marginRight: theme.spacing(1),
  },
}));

const NoDataText = styled(Typography)(({ theme }) => ({
  fontStyle: "italic",
  color: theme.palette.grey[500],
  marginLeft: theme.spacing(2),
}));

const HeaderIcon = styled(Avatar)(({ theme }) => ({
  backgroundColor: theme.palette.common.white,
  color: theme.palette.primary.main,
  marginRight: theme.spacing(1.5),
  width: 36,
  height: 36,
}));

const CompletionButton = styled(Button)(({ theme, completed }) => ({
  borderRadius: theme.spacing(1),
  fontWeight: 600,
  backgroundColor: completed
    ? theme.palette.success.main
    : theme.palette.warning.main,
  color: theme.palette.common.white,
  "&:hover": {
    backgroundColor: completed
      ? theme.palette.success.dark
      : theme.palette.warning.dark,
  },
  margin: theme.spacing(1),
  display: "block",
  width: "fit-content",
}));

const StyledDialog = styled(Dialog)(({ theme }) => ({
  "& .MuiDialog-paper": {
    borderRadius: theme.spacing(2),
    boxShadow: "0 10px 30px rgba(0, 0, 0, 0.2)",
    overflow: "hidden",
  },
}));

const PlansPage = () => {
  const [planSummaries, setPlanSummaries] = useState([]);
  const [loadingSummaries, setLoadingSummaries] = useState(true);
  const [selectedPlan, setSelectedPlan] = useState(null);
  const [loadingDetails, setLoadingDetails] = useState(false);
  const [openDialog, setOpenDialog] = useState(false);
  const [completionStatusMessage, setCompletionStatusMessage] = useState(null);

  useEffect(() => {
    const fetchPlanSummaries = async () => {
      try {
        const response = await api.get("/api/Ghosn/Plans/summaries");
        setPlanSummaries(response.data);
      } catch (error) {
        console.error("Error fetching plan summaries:", error);
      } finally {
        setLoadingSummaries(false);
      }
    };
    fetchPlanSummaries();
  }, []);

  const handleCloseDialog = () => {
    setOpenDialog(false);
    setSelectedPlan(null);
    setLoadingDetails(false);
  };

  const handleOpenDialog = async (planID) => {
    setLoadingDetails(true);
    setOpenDialog(true);
    try {
      const response = await api.get(`/api/Ghosn/Plan/PlanID/${planID}`);
      setSelectedPlan(response.data);
    } catch (error) {
      console.error("Error fetching plan details:", error);
    } finally {
      setLoadingDetails(false);
    }
  };

  const handleCompletePlan = async (planID) => {
    try {
      await api.put(`/api/Ghosn/Plan/SetAsCompleted/${planID}`);
      setPlanSummaries((prev) =>
        prev.map((summary) =>
          summary.planID === planID
            ? { ...summary, isCompleted: true }
            : summary
        )
      );
      setCompletionStatusMessage({
        message: "تم تحديد الخطة كمكتملة بنجاح",
        type: "success",
      });
      setTimeout(() => setCompletionStatusMessage(null), 3000);
    } catch (error) {
      console.error("Error marking plan as complete:", error);
      setCompletionStatusMessage({
        message: "حدث خطأ أثناء تحديد الخطة كمكتملة",
        type: "error",
      });
      setTimeout(() => setCompletionStatusMessage(null), 3000);
    }
  };

  if (loadingSummaries) {
    return (
      <Container>
        <Box
          sx={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            minHeight: "50vh",
          }}
        >
          <CircularProgress size={60} thickness={4} />
        </Box>
      </Container>
    );
  }

  const renderSteps = (title, steps) => (
    <>
      <SectionTitle variant="h6">{title}</SectionTitle>
      {steps?.length > 0 ? (
        steps.map((step, index) => (
          <StepText key={`${title}-${index}`} variant="body2">
            {step.step || step}
          </StepText>
        ))
      ) : (
        <NoDataText>لا توجد بيانات متاحة لهذا القسم.</NoDataText>
      )}
    </>
  );

  const formatUserInputs = (input) => {
    if (!input)
      return <NoDataText>لا توجد بيانات مدخلة من المستخدم.</NoDataText>;

    const mapToText = {
      locationType: {
        0: "حقل مفتوح",
        1: "حديقة منزلية",
        2: "سقف",
        3: "أوعية",
        4: "بيت زجاجي",
      },
      areaShape: { 0: "مربع", 1: "مستطيل", 2: "غير منتظم" },
      climate: { 0: "حار وجاف", 1: "معتدل", 2: "بارد", 3: "رطب", 4: "متغير" },
      temperature: { 0: "منخفضة", 1: "متوسطة", 2: "مرتفعة" },
      soilType: {
        0: "رملية",
        1: "طينية",
        2: "صخرية",
        3: "عضوية",
        4: "غير معروفة",
      },
      soilFertilityLevel: { 0: "منخفضة", 1: "متوسطة", 2: "مرتفعة" },
      plantsStatus: { 0: "سليمة", 1: "متوسطة", 2: "تحتاج إلى رعاية" },
      medicationsUsed: { 0: "لا شيء", 1: "سماد", 2: "مبيد حشري" },
    };

    const inputs = [
      `نوع مكان الزراعة: ${
        mapToText.locationType[input.locationType] || "غير محدد"
      }`,
      `شكل المنطقة: ${mapToText.areaShape[input.areaShape] || "غير محدد"}`,
      `المناخ: ${mapToText.climate[input.climate] || "غير محدد"}`,
      `متوسط درجة الحرارة: ${
        mapToText.temperature[input.temperature] || "غير محدد"
      }`,
      `نوع التربة: ${mapToText.soilType[input.soilType] || "غير محدد"}`,
      `خصوبة التربة: ${
        mapToText.soilFertilityLevel[input.soilFertilityLevel] || "غير محدد"
      }`,
      `حالة النباتات: ${
        mapToText.plantsStatus[input.plantsStatus] || "غير محدد"
      }`,
      `الأدوية المستخدمة: ${
        mapToText.medicationsUsed[input.medicationsUsed] || "غير محدد"
      }`,
      `مساحة المنطقة: ${input.areaSize || 0} متر مربع`,
      `النباتات المزروعة: ${
        input.currentlyPlantedPlants?.map((p) => p.plantName).join(", ") ||
        "لا يوجد"
      }`,
    ];

    return inputs.map((item, index) => (
      <StepText key={index} variant="body2">
        {item}
      </StepText>
    ));
  };

  return (
    <Container>
      <Typography
        variant="h3"
        align="center"
        sx={{ fontWeight: 700, color: "primary.main", mb: 5 }}
      >
        الخطط الزراعية
      </Typography>

      {completionStatusMessage && (
        <Alert
          severity={completionStatusMessage.type}
          sx={{ mb: 3, borderRadius: 2 }}
        >
          {completionStatusMessage.message}
        </Alert>
      )}

      {planSummaries.length === 0 ? (
        <Typography
          variant="h6"
          align="center"
          color="text.secondary"
          sx={{ mt: 4 }}
        >
          لا توجد خطط زراعية متاحة حاليًا
        </Typography>
      ) : (
        <Grid container spacing={3}>
          {planSummaries.map((summary, index) => (
            <Grid item xs={12} sm={6} md={4} key={index}>
              <StyledCard>
                <CardHeader>
                  <HeaderIcon>
                    <InfoIcon />
                  </HeaderIcon>
                  <Typography variant="h6">الخطة {index + 1}</Typography>
                </CardHeader>
                <CardContentStyled>
                  <Box
                    display="flex"
                    justifyContent="space-between"
                    alignItems="center"
                    mb={2}
                  >
                    <Typography variant="body1" color="text.secondary">
                      الحالة:
                    </Typography>
                    {summary.isCompleted ? (
                      <CheckCircleIcon sx={{ color: "success.main" }} />
                    ) : (
                      <CancelIcon sx={{ color: "error.main" }} />
                    )}
                  </Box>
                  <Box
                    display="flex"
                    justifyContent="space-between"
                    alignItems="center"
                  >
                    <Button
                      variant="outlined"
                      fullWidth
                      onClick={() => handleOpenDialog(summary.planID)}
                      sx={{ borderRadius: 1 }}
                    >
                      عرض التفاصيل
                    </Button>
                    <CompletionButton
                      completed={summary.isCompleted}
                      onClick={() => handleCompletePlan(summary.planID)}
                    >
                      {summary.isCompleted ? "مكتملة" : "تحديد كمكتملة"}
                    </CompletionButton>
                  </Box>
                </CardContentStyled>
              </StyledCard>
            </Grid>
          ))}
        </Grid>
      )}

      <StyledDialog
        open={openDialog}
        onClose={handleCloseDialog}
        fullWidth
        maxWidth="md"
        dir="rtl"
      >
        <DialogTitle
          sx={{
            bgcolor: "primary.main",
            color: "white",
            p: 2,
            display: "flex",
            justifyContent: "space-between",
          }}
        >
          تفاصيل الخطة
          <IconButton onClick={handleCloseDialog} sx={{ color: "white" }}>
            <CloseIcon />
          </IconButton>
        </DialogTitle>
        <DialogContent sx={{ p: 3 }}>
          {loadingDetails ? (
            <Box sx={{ textAlign: "center", py: 4 }}>
              <CircularProgress />
              <Typography variant="body2" color="text.secondary" mt={2}>
                جارٍ تحميل تفاصيل الخطة...
              </Typography>
            </Box>
          ) : selectedPlan ? (
            <>
              <CardHeader sx={{ mt: 3 }}>
                <HeaderIcon>
                  <InfoIcon />
                </HeaderIcon>
                <Typography variant="h6">بيانات المستخدم</Typography>
              </CardHeader>
              <CardContentStyled>
                {formatUserInputs(selectedPlan.input)}
              </CardContentStyled>

              <CardHeader>
                <HeaderIcon>
                  <PlantIcon />
                </HeaderIcon>
                <Typography variant="h6">النباتات المقترحة</Typography>
              </CardHeader>
              <CardContentStyled>
                {selectedPlan.output?.suggestedPlants?.map((plant, index) => (
                  <StepText key={index}>{plant.plantName}</StepText>
                )) || <NoDataText>لا توجد نباتات مقترحة.</NoDataText>}
              </CardContentStyled>

              <CardHeader>
                <HeaderIcon>
                  <ChecklistIcon />
                </HeaderIcon>
                <Typography variant="h6">خطوات الزراعة</Typography>
              </CardHeader>
              <CardContentStyled>
                {renderSteps(
                  "تحضير التربة",
                  selectedPlan.output?.plantingSteps?.prepareSoilSteps
                )}
                {renderSteps(
                  "اختيار النباتات",
                  selectedPlan.output?.plantingSteps?.choosePlants
                )}
                {renderSteps(
                  "الري",
                  selectedPlan.output?.plantingSteps?.wateringSteps
                )}
                {renderSteps(
                  "التسميد",
                  selectedPlan.output?.plantingSteps?.fertilizationSteps
                )}
                {renderSteps(
                  "العناية",
                  selectedPlan.output?.plantingSteps?.careSteps
                )}
              </CardContentStyled>

              <CardHeader>
                <HeaderIcon>
                  <TipsIcon />
                </HeaderIcon>
                <Typography variant="h6">نصائح إضافية</Typography>
              </CardHeader>
              <CardContentStyled>
                {renderSteps(
                  "تحسين التربة",
                  selectedPlan.output?.soilImprovements
                )}
                {renderSteps(
                  "الوقاية من الآفات",
                  selectedPlan.output?.pestPreventions
                )}
                {renderSteps(
                  "تناوب المحاصيل",
                  selectedPlan.output?.cropRotations
                )}
              </CardContentStyled>

              <CardHeader>
                <HeaderIcon>
                  <ScheduleIcon />
                </HeaderIcon>
                <Typography variant="h6">جدول زمني مقترح</Typography>
              </CardHeader>
              <CardContentStyled>
                {renderSteps(
                  "الأسابيع الأولى",
                  selectedPlan.output?.suggestedTimelines?.firstWeeks
                )}
                {renderSteps(
                  "الأسابيع الثانية",
                  selectedPlan.output?.suggestedTimelines?.secondWeeks
                )}
                {renderSteps(
                  "الأشهر الأولى",
                  selectedPlan.output?.suggestedTimelines?.firstMonths
                )}
                {renderSteps(
                  "الأشهر الثلاثة",
                  selectedPlan.output?.suggestedTimelines?.thirdMonths
                )}
              </CardContentStyled>

              <CardHeader>
                <HeaderIcon>
                  <MaterialsIcon />
                </HeaderIcon>
                <Typography variant="h6">المواد المطلوبة</Typography>
              </CardHeader>
              <CardContentStyled>
                {renderSteps(
                  "المواد المقترحة",
                  selectedPlan.output?.suggestedMaterials?.map((m) => ({
                    step: m.materialName,
                  }))
                )}
                {renderSteps(
                  "أدوات الزراعة",
                  selectedPlan.output?.suggestedFarmingTools?.map((t) => ({
                    step: t.farmingToolName,
                  }))
                )}
                {renderSteps(
                  "أنظمة الري",
                  selectedPlan.output?.suggestedIrrigationSystems?.map((s) => ({
                    step: s.irrigationSystemName,
                  }))
                )}
              </CardContentStyled>
            </>
          ) : (
            <Typography color="error">فشل في تحميل تفاصيل الخطة.</Typography>
          )}
        </DialogContent>
      </StyledDialog>
    </Container>
  );
};

export default PlansPage;
