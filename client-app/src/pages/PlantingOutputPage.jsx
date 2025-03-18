import React, { useState, useEffect } from "react";
import {
  Card,
  CardContent,
  Typography,
  Button,
  Box,
  Divider,
  Grid,
  IconButton,
  CircularProgress,
} from "@mui/material";
import {
  LocalFlorist as PlantIcon,
  Checklist as ChecklistIcon,
  TipsAndUpdates as TipsIcon,
  CalendarMonth as ScheduleIcon,
  Build as MaterialsIcon,
  Info as InfoIcon,
} from "@mui/icons-material";
import { useNavigate, useLocation } from "react-router-dom";
import axios from "axios";

const PlantingOutputPage = () => {
  const navigate = useNavigate();
  const location = useLocation(); // Get location object
  const [loading, setLoading] = useState(true);
  const [aiOutput, setAiOutput] = useState(null);
  const [userInputs, setUserInputs] = useState(null); // to add userInputs

  const handleChatClick = () => {
    navigate("/chat");
  };

  useEffect(() => {
    const inputData = location.state?.formData;
    setUserInputs(inputData);

    const fetchData = async () => {
      if (inputData) {
        try {
          const response = await axios.post(
            "/api/Ghosn/GeneratePlan",
            inputData
          );

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

  if (loading) {
    return (
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
    );
  }

  // Helper function to format user inputs.  Makes the code below cleaner.
  const formatUserInputs = () => {
    if (!userInputs) {
      return <Typography>No user input data available.</Typography>;
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

    const inputs = [];

    inputs.push(
      `نوع مكان الزراعة: ${
        mapToText.locationType[userInputs.locationType] || "غير محدد"
      }`
    );
    inputs.push(
      `شكل المنطقة: ${mapToText.areaShape[userInputs.areaShape] || "غير محدد"}`
    );
    inputs.push(
      `المناخ: ${mapToText.climate[userInputs.climate] || "غير محدد"}`
    );
    inputs.push(
      `متوسط درجة الحرارة: ${
        mapToText.temperature[userInputs.temperature] || "غير محدد"
      }`
    );
    inputs.push(
      `نوع التربة: ${mapToText.soilType[userInputs.soilType] || "غير محدد"}`
    );
    inputs.push(
      `خصوبة التربة: ${
        mapToText.soilFertility[userInputs.soilFertility] || "غير محدد"
      }`
    );
    inputs.push(
      `حالة النباتات: ${
        mapToText.plantHealth[userInputs.plantHealth] || "غير محدد"
      }`
    );
    inputs.push(
      `الأدوية المستخدمة: ${
        mapToText.pesticide[userInputs.pesticide] || "غير محدد"
      }`
    );
    inputs.push(`مساحة المنطقة: ${userInputs.areaSize || 0} متر مربع`);
    inputs.push(
      `النباتات المزروعة: ${
        userInputs.currentlyPlantedPlants
          ?.map((plant) => plant.plantName)
          .join(", ") || "لا يوجد"
      }`
    );

    return inputs.map((input, index) => (
      <Typography key={index} variant="body1" sx={{ mb: 0.5 }}>
        {input}
      </Typography>
    ));
  };

  return (
    <Box sx={{ p: 3 }} dir="rtl">
      <Typography variant="h4" gutterBottom color="primary">
        خطة الزراعة المخصصة لك
      </Typography>
      <Divider sx={{ mb: 4 }} />

      <Grid container spacing={3}>
        <Grid item xs={12} md={6}>
          <Card elevation={3} sx={{ borderRadius: 2 }}>
            <CardContent>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <InfoIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6" gutterBottom>
                  بيانات المستخدم:
                </Typography>
              </Box>
              {formatUserInputs()}
            </CardContent>
          </Card>
        </Grid>

        <Grid item xs={12} md={6}>
          <Card elevation={3} sx={{ borderRadius: 2 }}>
            <CardContent>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <PlantIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6" gutterBottom>
                  النباتات المقترحة:
                </Typography>
              </Box>
              {aiOutput?.suggestedPlants?.map((plant, index) => (
                <Typography key={index} variant="body1" sx={{ mb: 0.5 }}>
                  {plant.plantName}
                </Typography>
              )) || <Typography>لا توجد نباتات مقترحة.</Typography>}
            </CardContent>
          </Card>
        </Grid>

        <Grid item xs={12}>
          <Card elevation={3} sx={{ borderRadius: 2 }}>
            <CardContent>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <ChecklistIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6" gutterBottom>
                  خطوات الزراعة:
                </Typography>
              </Box>
              {/* Prepare Soil Steps */}
              <Typography variant="body1" sx={{ mb: 0.5, fontWeight: "bold" }}>
                تحضير التربة:
              </Typography>
              {aiOutput?.plantingSteps?.prepareSoilSteps?.map((step, index) => (
                <Typography
                  key={`prepareSoil-${index}`}
                  variant="body2"
                  sx={{ mb: 0.5, ml: 2 }}
                >
                  - {step.step}
                </Typography>
              )) || <Typography>لا توجد خطوات لتحضير التربة.</Typography>}

              {/* Choose Plants Steps */}
              <Typography
                variant="body1"
                sx={{ mb: 0.5, mt: 2, fontWeight: "bold" }}
              >
                اختيار النباتات:
              </Typography>
              {aiOutput?.plantingSteps?.choosePlants?.map((step, index) => (
                <Typography
                  key={`choosePlants-${index}`}
                  variant="body2"
                  sx={{ mb: 0.5, ml: 2 }}
                >
                  - {step.step}
                </Typography>
              )) || <Typography>لا توجد خطوات لاختيار النباتات.</Typography>}

              {/* Watering Steps */}
              <Typography
                variant="body1"
                sx={{ mb: 0.5, mt: 2, fontWeight: "bold" }}
              >
                الري:
              </Typography>
              {aiOutput?.plantingSteps?.wateringSteps?.map((step, index) => (
                <Typography
                  key={`watering-${index}`}
                  variant="body2"
                  sx={{ mb: 0.5, ml: 2 }}
                >
                  - {step.step}
                </Typography>
              )) || <Typography>لا توجد خطوات للري.</Typography>}

              {/* Fertilization Steps */}
              <Typography
                variant="body1"
                sx={{ mb: 0.5, mt: 2, fontWeight: "bold" }}
              >
                التسميد:
              </Typography>
              {aiOutput?.plantingSteps?.fertilizationSteps?.map(
                (step, index) => (
                  <Typography
                    key={`fertilization-${index}`}
                    variant="body2"
                    sx={{ mb: 0.5, ml: 2 }}
                  >
                    - {step.step}
                  </Typography>
                )
              ) || <Typography>لا توجد خطوات للتسميد.</Typography>}

              {/* Care Steps */}
              <Typography
                variant="body1"
                sx={{ mb: 0.5, mt: 2, fontWeight: "bold" }}
              >
                العناية:
              </Typography>
              {aiOutput?.plantingSteps?.careSteps?.map((step, index) => (
                <Typography
                  key={`care-${index}`}
                  variant="body2"
                  sx={{ mb: 0.5, ml: 2 }}
                >
                  - {step.step}
                </Typography>
              )) || <Typography>لا توجد خطوات للعناية.</Typography>}
            </CardContent>
          </Card>
        </Grid>

        <Grid item xs={12} md={6}>
          <Card elevation={3} sx={{ borderRadius: 2 }}>
            <CardContent>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <TipsIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6" gutterBottom>
                  نصائح إضافية:
                </Typography>
              </Box>
              {/* Soil Improvements */}
              <Typography variant="body1" sx={{ mb: 0.5, fontWeight: "bold" }}>
                تحسين التربة:
              </Typography>
              {aiOutput?.soilImprovements?.map((tip, index) => (
                <Typography
                  key={`soilImprovement-${index}`}
                  variant="body2"
                  sx={{ mb: 0.5, ml: 2 }}
                >
                  - {tip.step}
                </Typography>
              )) || <Typography>لا توجد نصائح لتحسين التربة.</Typography>}

              {/* Pest Preventions */}
              <Typography
                variant="body1"
                sx={{ mb: 0.5, mt: 2, fontWeight: "bold" }}
              >
                الوقاية من الآفات:
              </Typography>
              {aiOutput?.pestPreventions?.map((tip, index) => (
                <Typography
                  key={`pestPrevention-${index}`}
                  variant="body2"
                  sx={{ mb: 0.5, ml: 2 }}
                >
                  - {tip.step}
                </Typography>
              )) || <Typography>لا توجد نصائح للوقاية من الآفات.</Typography>}
              {/* Crop Rotations */}
              <Typography variant="body1" sx={{ mb: 0.5, fontWeight: "bold" }}>
                تناوب المحاصيل:
              </Typography>
              {aiOutput?.cropRotations?.map((tip, index) => (
                <Typography
                  key={`cropRotations-${index}`}
                  variant="body2"
                  sx={{ mb: 0.5, ml: 2 }}
                >
                  - {tip.step}
                </Typography>
              )) || <Typography>لا توجد نصائح لتناوب المحاصيل.</Typography>}
            </CardContent>
          </Card>
        </Grid>

        <Grid item xs={12} md={6}>
          <Card elevation={3} sx={{ borderRadius: 2 }}>
            <CardContent>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <ScheduleIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6" gutterBottom>
                  جدول زمني مقترح:
                </Typography>
              </Box>
              {/* First Weeks */}
              <Typography variant="body1" sx={{ mb: 0.5, fontWeight: "bold" }}>
                الأسابيع الأولى:
              </Typography>
              {aiOutput?.suggestedTimelines?.firstWeeks?.map((item, index) => (
                <Typography
                  key={`firstWeeks-${index}`}
                  variant="body2"
                  sx={{ mb: 0.5, ml: 2 }}
                >
                  - {item.step}
                </Typography>
              )) || <Typography>لا توجد خطوات للأسبوع الأول.</Typography>}

              {/* Second Weeks */}
              <Typography
                variant="body1"
                sx={{ mb: 0.5, mt: 2, fontWeight: "bold" }}
              >
                الأسابيع الثانية:
              </Typography>
              {aiOutput?.suggestedTimelines?.secondWeeks?.map((item, index) => (
                <Typography
                  key={`secondWeeks-${index}`}
                  variant="body2"
                  sx={{ mb: 0.5, ml: 2 }}
                >
                  - {item.step}
                </Typography>
              )) || <Typography>لا توجد خطوات للأسبوع الثاني.</Typography>}

              {/* First Months */}
              <Typography
                variant="body1"
                sx={{ mb: 0.5, mt: 2, fontWeight: "bold" }}
              >
                الأشهر الأولى:
              </Typography>
              {aiOutput?.suggestedTimelines?.firstMonths?.map((item, index) => (
                <Typography
                  key={`firstMonths-${index}`}
                  variant="body2"
                  sx={{ mb: 0.5, ml: 2 }}
                >
                  - {item.step}
                </Typography>
              )) || <Typography>لا توجد خطوات للشهر الأول.</Typography>}

              {/* Third Months */}
              <Typography
                variant="body1"
                sx={{ mb: 0.5, mt: 2, fontWeight: "bold" }}
              >
                الأشهر الثلاثة:
              </Typography>
              {aiOutput?.suggestedTimelines?.thirdMonths?.map((item, index) => (
                <Typography
                  key={`thirdMonths-${index}`}
                  variant="body2"
                  sx={{ mb: 0.5, ml: 2 }}
                >
                  - {item.step}
                </Typography>
              )) || <Typography>لا توجد خطوات للشهر الثالث.</Typography>}
            </CardContent>
          </Card>
        </Grid>

        <Grid item xs={12} md={6}>
          <Card elevation={3} sx={{ borderRadius: 2 }}>
            <CardContent>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <MaterialsIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6" gutterBottom>
                  المواد المطلوبة:
                </Typography>
              </Box>

              {/* Suggested Materials */}
              <Typography variant="body1" sx={{ mb: 0.5, fontWeight: "bold" }}>
                المواد المقترحة:
              </Typography>
              {aiOutput?.suggestedMaterials?.map((material, index) => (
                <Typography
                  key={`material-${index}`}
                  variant="body2"
                  sx={{ mb: 0.5, ml: 2 }}
                >
                  - {material.materialName}
                </Typography>
              )) || <Typography>لا توجد مواد مقترحة.</Typography>}

              {/* Suggested Farming Tools */}
              <Typography
                variant="body1"
                sx={{ mb: 0.5, mt: 2, fontWeight: "bold" }}
              >
                أدوات الزراعة المقترحة:
              </Typography>
              {aiOutput?.suggestedFarmingTools?.map((tool, index) => (
                <Typography
                  key={`tool-${index}`}
                  variant="body2"
                  sx={{ mb: 0.5, ml: 2 }}
                >
                  - {tool.farmingToolName}
                </Typography>
              )) || <Typography>لا توجد أدوات زراعة مقترحة.</Typography>}

              {/* Suggested Irrigation Systems */}
              <Typography
                variant="body1"
                sx={{ mb: 0.5, mt: 2, fontWeight: "bold" }}
              >
                أنظمة الري المقترحة:
              </Typography>
              {aiOutput?.suggestedIrrigationSystems?.map((system, index) => (
                <Typography
                  key={`system-${index}`}
                  variant="body2"
                  sx={{ mb: 0.5, ml: 2 }}
                >
                  - {system.irrigationSystemName}
                </Typography>
              )) || <Typography>لا توجد أنظمة ري مقترحة.</Typography>}
            </CardContent>
          </Card>
        </Grid>
      </Grid>

      <Button
        variant="contained"
        color="primary"
        onClick={handleChatClick}
        sx={{ mt: 3, px: 4, py: 1.5, borderRadius: 8 }}
      >
        التحدث مع المخرجات
      </Button>
    </Box>
  );
};

export default PlantingOutputPage;
