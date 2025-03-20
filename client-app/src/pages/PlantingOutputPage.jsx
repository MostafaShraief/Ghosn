// client-app/src/pages/PlantingOutputPage.jsx
import React, { useState, useEffect } from "react";
import {
  Card,
  CardContent,
  Typography,
  Button,
  Box,
  Divider,
  Grid,
  CircularProgress,
  useMediaQuery,
  Avatar,
  Snackbar, // Import Snackbar
  Alert, // Import Alert
} from "@mui/material";
import {
  LocalFlorist as PlantIcon,
  Checklist as ChecklistIcon,
  TipsAndUpdates as TipsIcon,
  CalendarMonth as ScheduleIcon,
  Build as MaterialsIcon,
  Info as InfoIcon,
  Save as SaveIcon, // Icon for the save button
} from "@mui/icons-material";
import { useNavigate, useLocation } from "react-router-dom";
import api from "@/services/api";
import { styled } from "@mui/system";

// --- Styled Components ---

const Container = styled(Box)(({ theme }) => ({
  minHeight: "100vh",
  [theme.breakpoints.down("md")]: {
    padding: theme.spacing(2),
  },
}));

const StyledCard = styled(Card)(({ theme }) => ({
  borderRadius: theme.spacing(2.5),
  boxShadow: "0 6px 12px rgba(0, 0, 0, 0.08)",
  transition: "transform 0.2s ease-in-out",
  "&:hover": {
    transform: "translateY(-6px)",
  },
  overflow: "hidden",
  width: "100%",
  display: "flex",
  flexDirection: "column",
  border: `1px solid ${theme.palette.grey[200]}`,
}));

const CardHeader = styled(Box)(({ theme }) => ({
  display: "flex",
  alignItems: "center",
  padding: theme.spacing(2),
  backgroundColor: theme.palette.primary.main,
  color: theme.palette.common.white,
  borderRadius: `${theme.spacing(2.5)} ${theme.spacing(2.5)} 0 0`,
}));

const CardContentStyled = styled(CardContent)(({ theme }) => ({
  flexGrow: 1,
  padding: theme.spacing(3),
}));

const SectionTitle = styled(Typography)(({ theme }) => ({
  fontWeight: 600,
  color: theme.palette.primary.dark,
  marginBottom: theme.spacing(1.5),
  marginTop: theme.spacing(2), // Add top margin for spacing between sections
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
  color: theme.palette.text.disabled,
  marginLeft: theme.spacing(3), // Indent "No Data" text
}));

const HeaderIcon = styled(Avatar)(({ theme }) => ({
  backgroundColor: theme.palette.common.white,
  color: theme.palette.primary.main,
  marginRight: theme.spacing(2),
}));

const PlantingOutputPage = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const [loading, setLoading] = useState(true);
  const [aiOutput, setAiOutput] = useState(null);
  const [userInputs, setUserInputs] = useState(null);
  const isMdDown = useMediaQuery((theme) => theme.breakpoints.down("md"));
  const [snackbarOpen, setSnackbarOpen] = useState(false); // State for Snackbar
  const [snackbarMessage, setSnackbarMessage] = useState(""); // Message for Snackbar
  const [snackbarSeverity, setSnackbarSeverity] = useState("success"); // Severity for Snackbar

  const handleChatClick = () => {
    navigate("/chat");
  };

  useEffect(() => {
    const inputData = location.state?.formData;
    setUserInputs(inputData);

    const fetchData = async () => {
      if (inputData) {
        try {
          const response = await api.post("/api/Ghosn/GeneratePlan", inputData);
          setAiOutput(response.data);
          setLoading(false);
        } catch (error) {
          console.error("Error fetching data:", error);
          setLoading(false);
        }
      } else {
        setLoading(false);
      }
    };
    fetchData();
  }, [location.state]);

  const handleSavePlan = async () => {
    if (!aiOutput) {
      setSnackbarMessage("لا توجد خطة لحفظها.");
      setSnackbarSeverity("error");
      setSnackbarOpen(true);
      return;
    }

    // Assuming you have a client ID. Replace '123' with the actual client ID.
    // You might get the client ID from user authentication or context.
    const clientId = 2;

    const payload = {
      planID: 0, // Usually, the API handles ID generation on creation, so it could be 0 or omitted.
      clientID: clientId,
      output: aiOutput, // Send the generated plan data directly.
      input: userInputs,
    };

    try {
      const response = await api.post(`/api/Ghosn/Plan/${clientId}`, payload);
      console.log("Plan saved:", response.data);
      setSnackbarMessage("تم حفظ الخطة بنجاح!");
      setSnackbarSeverity("success");
      setSnackbarOpen(true);
    } catch (error) {
      console.error("Error saving plan:", error.response || error);
      setSnackbarMessage(
        `حدث خطأ أثناء حفظ الخطة: ${
          error.response?.data?.message || error.message || "Unknown error"
        }`
      );
      setSnackbarSeverity("error");
      setSnackbarOpen(true);
    }
  };

  const handleCloseSnackbar = (event, reason) => {
    if (reason === "clickaway") {
      return;
    }
    setSnackbarOpen(false);
  };

  if (loading) {
    return (
      <Container>
        <Box
          sx={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            height: "50vh",
          }}
        >
          <CircularProgress />
        </Box>
      </Container>
    );
  }
  const formatUserInputs = () => {
    if (!userInputs) {
      return <NoDataText>No user input data available.</NoDataText>;
    }

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
      soilFertility: { 0: "منخفضة", 1: "متوسطة", 2: "مرتفعة" },
      plantHealth: { 0: "سليمة", 1: "متوسطة", 2: "تحتاج إلى رعاية" },
      pesticide: { 0: "لا شيء", 1: "سماد", 2: "مبيد حشري" },
    };

    const inputs = [
      `نوع مكان الزراعة: ${
        mapToText.locationType[userInputs.locationType] || "غير محدد"
      }`,
      `شكل المنطقة: ${mapToText.areaShape[userInputs.areaShape] || "غير محدد"}`,
      `المناخ: ${mapToText.climate[userInputs.climate] || "غير محدد"}`,
      `متوسط درجة الحرارة: ${
        mapToText.temperature[userInputs.temperature] || "غير محدد"
      }`,
      `نوع التربة: ${mapToText.soilType[userInputs.soilType] || "غير محدد"}`,
      `خصوبة التربة: ${
        mapToText.soilFertility[userInputs.soilFertility] || "غير محدد"
      }`,
      `حالة النباتات: ${
        mapToText.plantHealth[userInputs.plantHealth] || "غير محدد"
      }`,
      `الأدوية المستخدمة: ${
        mapToText.pesticide[userInputs.pesticide] || "غير محدد"
      }`,
      `مساحة المنطقة: ${userInputs.areaSize || 0} متر مربع`,
      `النباتات المزروعة: ${
        userInputs.currentlyPlantedPlants
          ?.map((plant) => plant.plantName)
          .join(", ") || "لا يوجد"
      }`,
    ];

    return inputs.map((input, index) => (
      <StepText key={index} variant="body1">
        {input}
      </StepText>
    ));
  };

  // Helper function to render steps (now without Accordion)
  const renderSteps = (title, steps) => (
    <>
      <SectionTitle variant="h6">{title}</SectionTitle>
      {steps && steps.length > 0 ? (
        steps.map((step, index) => (
          <StepText key={`${title}-${index}`} variant="body2">
            {step.step}
          </StepText>
        ))
      ) : (
        <NoDataText>لا توجد خطوات {title.toLowerCase()}.</NoDataText>
      )}
    </>
  );

  return (
    <Container dir="rtl">
      <Typography
        variant="h4"
        gutterBottom
        color="primary"
        align="center"
        sx={{ fontWeight: "bold", mb: 4 }}
      >
        خطة الزراعة المخصصة لك
      </Typography>

      <Grid container spacing={3}>
        {/* User Inputs */}
        <Grid item xs={12}>
          <StyledCard>
            <CardHeader>
              <HeaderIcon>
                <InfoIcon />
              </HeaderIcon>
              <Typography variant="h6">بيانات المستخدم</Typography>
            </CardHeader>
            <CardContentStyled>{formatUserInputs()}</CardContentStyled>
          </StyledCard>
        </Grid>

        {/* Suggested Plants */}
        <Grid item xs={12}>
          <StyledCard>
            <CardHeader>
              <HeaderIcon>
                <PlantIcon />
              </HeaderIcon>
              <Typography variant="h6">النباتات المقترحة</Typography>
            </CardHeader>
            <CardContentStyled>
              {aiOutput?.suggestedPlants?.length > 0 ? (
                aiOutput.suggestedPlants.map((plant, index) => (
                  <StepText key={index} variant="body1">
                    {plant.plantName}
                  </StepText>
                ))
              ) : (
                <NoDataText>لا توجد نباتات مقترحة.</NoDataText>
              )}
            </CardContentStyled>
          </StyledCard>
        </Grid>

        {/* Planting Steps */}
        <Grid item xs={12}>
          <StyledCard>
            <CardHeader>
              <HeaderIcon>
                <ChecklistIcon />
              </HeaderIcon>
              <Typography variant="h6">خطوات الزراعة</Typography>
            </CardHeader>
            <CardContentStyled>
              {renderSteps(
                "تحضير التربة",
                aiOutput?.plantingSteps?.prepareSoilSteps
              )}
              {renderSteps(
                "اختيار النباتات",
                aiOutput?.plantingSteps?.choosePlants
              )}
              {renderSteps("الري", aiOutput?.plantingSteps?.wateringSteps)}
              {renderSteps(
                "التسميد",
                aiOutput?.plantingSteps?.fertilizationSteps
              )}
              {renderSteps("العناية", aiOutput?.plantingSteps?.careSteps)}
            </CardContentStyled>
          </StyledCard>
        </Grid>

        {/* Additional Tips */}
        <Grid item xs={12}>
          <StyledCard>
            <CardHeader>
              <HeaderIcon>
                <TipsIcon />
              </HeaderIcon>
              <Typography variant="h6">نصائح إضافية</Typography>
            </CardHeader>
            <CardContentStyled>
              {renderSteps("تحسين التربة", aiOutput?.soilImprovements)}
              {renderSteps("الوقاية من الآفات", aiOutput?.pestPreventions)}
              {renderSteps("تناوب المحاصيل", aiOutput?.cropRotations)}
            </CardContentStyled>
          </StyledCard>
        </Grid>

        {/* Suggested Timeline */}
        <Grid item xs={12}>
          <StyledCard>
            <CardHeader>
              <HeaderIcon>
                <ScheduleIcon />
              </HeaderIcon>
              <Typography variant="h6">جدول زمني مقترح</Typography>
            </CardHeader>
            <CardContentStyled>
              {renderSteps(
                "الأسابيع الأولى",
                aiOutput?.suggestedTimelines?.firstWeeks
              )}
              {renderSteps(
                "الأسابيع الثانية",
                aiOutput?.suggestedTimelines?.secondWeeks
              )}
              {renderSteps(
                "الأشهر الأولى",
                aiOutput?.suggestedTimelines?.firstMonths
              )}
              {renderSteps(
                "الأشهر الثلاثة",
                aiOutput?.suggestedTimelines?.thirdMonths
              )}
            </CardContentStyled>
          </StyledCard>
        </Grid>

        {/* Required Materials */}
        <Grid item xs={12}>
          <StyledCard>
            <CardHeader>
              <HeaderIcon>
                <MaterialsIcon />
              </HeaderIcon>
              <Typography variant="h6">المواد المطلوبة</Typography>
            </CardHeader>
            <CardContentStyled>
              {renderSteps(
                "المواد المقترحة",
                aiOutput?.suggestedMaterials?.map((m) => ({
                  step: m.materialName,
                }))
              )}
              {renderSteps(
                "أدوات الزراعة المقترحة",
                aiOutput?.suggestedFarmingTools?.map((t) => ({
                  step: t.farmingToolName,
                }))
              )}
              {renderSteps(
                "أنظمة الري المقترحة",
                aiOutput?.suggestedIrrigationSystems?.map((s) => ({
                  step: s.irrigationSystemName,
                }))
              )}
            </CardContentStyled>
          </StyledCard>
        </Grid>
      </Grid>

      <Box sx={{ display: "flex", gap: 2, mt: 4, justifyContent: "center" }}>
        <Button
          variant="contained"
          color="primary"
          onClick={handleChatClick}
          startIcon={<MaterialsIcon />} // You can replace MaterialsIcon with a suitable chat icon.
          sx={{
            px: 5,
            py: 1.5,
            borderRadius: 25,
            fontSize: "1.1rem",
            textTransform: "none",
            fontWeight: "bold",
            boxShadow: "0 2px 5px rgba(0, 0, 0, 0.2)",
          }}
        >
          التحدث مع المخرجات
        </Button>
        <Button
          variant="contained"
          color="secondary" // Use a different color to distinguish
          onClick={handleSavePlan}
          startIcon={<SaveIcon />}
          sx={{
            px: 5,
            py: 1.5,
            borderRadius: 25,
            fontSize: "1.1rem",
            textTransform: "none",
            fontWeight: "bold",
            boxShadow: "0 2px 5px rgba(0, 0, 0, 0.2)",
          }}
        >
          حفظ الخطة
        </Button>
      </Box>
      <Snackbar
        open={snackbarOpen}
        autoHideDuration={6000}
        onClose={handleCloseSnackbar}
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
      >
        <Alert
          onClose={handleCloseSnackbar}
          severity={snackbarSeverity}
          sx={{ width: "100%" }}
        >
          {snackbarMessage}
        </Alert>
      </Snackbar>
    </Container>
  );
};

export default PlantingOutputPage;
