import React, { useState, useEffect } from "react";
import {
  Card,
  CardContent,
  Typography,
  Box,
  Grid,
  CircularProgress,
  Avatar,
  Accordion,
  AccordionSummary,
  AccordionDetails,
} from "@mui/material";
import {
  ExpandMore as ExpandMoreIcon,
  LocalFlorist as PlantIcon,
  Checklist as ChecklistIcon,
  TipsAndUpdates as TipsIcon,
  CalendarMonth as ScheduleIcon,
  Build as MaterialsIcon,
  Info as InfoIcon,
} from "@mui/icons-material";
import { styled } from "@mui/system";
import api from "@/services/api";

// --- Styled Components ---

const Container = styled(Box)(({ theme }) => ({
  minHeight: "100vh",
  padding: theme.spacing(4), // Add some padding around the container
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
  overflow: "hidden", // Handles Accordion overflow
  width: "100%",
  display: "flex",
  flexDirection: "column", // Stack header and content vertically
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
  flexGrow: 1, // Allow content to grow and fill available space
  padding: theme.spacing(3),
}));

const SectionTitle = styled(Typography)(({ theme }) => ({
  fontWeight: 600,
  color: theme.palette.primary.dark,
  marginBottom: theme.spacing(1.5),
  marginTop: theme.spacing(2),
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
  marginLeft: theme.spacing(3),
}));

const HeaderIcon = styled(Avatar)(({ theme }) => ({
  backgroundColor: theme.palette.common.white,
  color: theme.palette.primary.main,
  marginLeft: theme.spacing(2),
}));

const PlansPage = () => {
  const [plans, setPlans] = useState([]);
  const [loading, setLoading] = useState(true);
  const [expanded, setExpanded] = useState(false);

  const handleChange = (panel) => (event, isExpanded) => {
    setExpanded(isExpanded ? panel : false);
  };

  useEffect(() => {
    const fetchPlans = async () => {
      try {
        const response = await api.get("/api/Ghosn/AllPlans");
        setPlans(response.data);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching plans:", error);
        setLoading(false);
      }
    };
    fetchPlans();
  }, []);

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

  // Helper function to render steps, now with Accordion
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

  const formatUserInputs = (input) => {
    if (!input) {
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
        input.currentlyPlantedPlants
          ?.map((plant) => plant.plantName)
          .join(", ") || "لا يوجد"
      }`,
    ];

    return inputs.map((inputItem, index) => (
      <StepText key={index} variant="body1">
        {inputItem}
      </StepText>
    ));
  };

  return (
    <Container dir="rtl">
      <Typography
        variant="h4"
        gutterBottom
        color="primary"
        align="center"
        sx={{ fontWeight: "bold", mb: 4 }}
      >
        الخطط الزراعية
      </Typography>

      {plans.length === 0 ? (
        <Typography variant="h6" align="center" color="textSecondary">
          لا توجد خطط زراعية متاحة حاليًا.
        </Typography>
      ) : (
        <Grid container spacing={3}>
          {plans.map((plan, planIndex) => (
            <Grid item xs={12} key={planIndex}>
              <StyledCard>
                <Accordion
                  expanded={expanded === `panel${planIndex}`}
                  onChange={handleChange(`panel${planIndex}`)}
                >
                  <AccordionSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls={`panel${planIndex}-content`}
                    id={`panel${planIndex}-header`}
                  >
                    <Typography variant="h6">الخطة {planIndex + 1}</Typography>
                  </AccordionSummary>
                  <AccordionDetails>
                    {/* User Inputs */}
                    <CardHeader>
                      <HeaderIcon>
                        <InfoIcon />
                      </HeaderIcon>
                      <Typography variant="h6">بيانات المستخدم</Typography>
                    </CardHeader>
                    <CardContentStyled>
                      {formatUserInputs(plan.input)}
                    </CardContentStyled>

                    {/* Suggested Plants */}
                    <CardHeader>
                      <HeaderIcon>
                        <PlantIcon />
                      </HeaderIcon>
                      <Typography variant="h6">النباتات المقترحة</Typography>
                    </CardHeader>
                    <CardContentStyled>
                      {plan.output?.suggestedPlants?.length > 0 ? (
                        plan.output.suggestedPlants.map((plant, index) => (
                          <StepText key={index} variant="body1">
                            {plant.plantName}
                          </StepText>
                        ))
                      ) : (
                        <NoDataText>لا توجد نباتات مقترحة.</NoDataText>
                      )}
                    </CardContentStyled>

                    {/* Planting Steps */}

                    <CardHeader>
                      <HeaderIcon>
                        <ChecklistIcon />
                      </HeaderIcon>
                      <Typography variant="h6">خطوات الزراعة</Typography>
                    </CardHeader>
                    <CardContentStyled>
                      {renderSteps(
                        "تحضير التربة",
                        plan.output?.plantingSteps?.prepareSoilSteps
                      )}
                      {renderSteps(
                        "اختيار النباتات",
                        plan.output?.plantingSteps?.choosePlants
                      )}
                      {renderSteps(
                        "الري",
                        plan.output?.plantingSteps?.wateringSteps
                      )}
                      {renderSteps(
                        "التسميد",
                        plan.output?.plantingSteps?.fertilizationSteps
                      )}
                      {renderSteps(
                        "العناية",
                        plan.output?.plantingSteps?.careSteps
                      )}
                    </CardContentStyled>

                    {/* Additional Tips */}
                    <CardHeader>
                      <HeaderIcon>
                        <TipsIcon />
                      </HeaderIcon>
                      <Typography variant="h6">نصائح إضافية</Typography>
                    </CardHeader>
                    <CardContentStyled>
                      {renderSteps(
                        "تحسين التربة",
                        plan.output?.soilImprovements
                      )}
                      {renderSteps(
                        "الوقاية من الآفات",
                        plan.output?.pestPreventions
                      )}
                      {renderSteps(
                        "تناوب المحاصيل",
                        plan.output?.cropRotations
                      )}
                    </CardContentStyled>

                    {/* Suggested Timeline */}
                    <CardHeader>
                      <HeaderIcon>
                        <ScheduleIcon />
                      </HeaderIcon>
                      <Typography variant="h6">جدول زمني مقترح</Typography>
                    </CardHeader>
                    <CardContentStyled>
                      {renderSteps(
                        "الأسابيع الأولى",
                        plan.output?.suggestedTimelines?.firstWeeks
                      )}
                      {renderSteps(
                        "الأسابيع الثانية",
                        plan.output?.suggestedTimelines?.secondWeeks
                      )}
                      {renderSteps(
                        "الأشهر الأولى",
                        plan.output?.suggestedTimelines?.firstMonths
                      )}
                      {renderSteps(
                        "الأشهر الثلاثة",
                        plan.output?.suggestedTimelines?.thirdMonths
                      )}
                    </CardContentStyled>

                    {/* Required Materials */}
                    <CardHeader>
                      <HeaderIcon>
                        <MaterialsIcon />
                      </HeaderIcon>
                      <Typography variant="h6">المواد المطلوبة</Typography>
                    </CardHeader>

                    <CardContentStyled>
                      {renderSteps(
                        "المواد المقترحة",
                        plan.output?.suggestedMaterials?.map((m) => ({
                          step: m.materialName,
                        }))
                      )}
                      {renderSteps(
                        "أدوات الزراعة المقترحة",
                        plan.output?.suggestedFarmingTools?.map((t) => ({
                          step: t.farmingToolName,
                        }))
                      )}
                      {renderSteps(
                        "أنظمة الري المقترحة",
                        plan.output?.suggestedIrrigationSystems?.map((s) => ({
                          step: s.irrigationSystemName,
                        }))
                      )}
                    </CardContentStyled>
                  </AccordionDetails>
                </Accordion>
              </StyledCard>
            </Grid>
          ))}
        </Grid>
      )}
    </Container>
  );
};

export default PlansPage;
