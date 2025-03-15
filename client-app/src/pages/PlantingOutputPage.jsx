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
import { useNavigate } from "react-router-dom";

const PlantingOutputPage = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(true);
  const [aiOutput, setAiOutput] = useState(null);

  const handleChatClick = () => {
    navigate("/chat");
  };

  // Hardcoded AI output
  const mockAiOutput = {
    title: "خطة الزراعة المخصصة لك",
    userInputs: [
      "نوع المكان: حديقة منزلية",
      "المساحة: 20 متر مربع",
      "المناخ: معتدل",
      "نوع التربة: طينية",
      "الإضاءة: مشمس مباشر (6 ساعات يوميًا)",
      "الوقت المتاح للعناية: 1-3 ساعات أسبوعيًا",
      "الهدف من الزراعة: تجميل المكان وإنتاج محاصيل غذائية",
    ],
    recommendations: [
      "نباتات زينة: ورد الجوري، الياسمين، الأزاليا.",
      "نباتات غذائية: طماطم، خس، فلفل.",
      "نباتات عطرية: نعناع، ريحان، إكليل الجبل.",
    ],
    steps: [
      "قم بتحضير التربة بخلطها مع السماد العضوي.",
      "ازرع الشتلات في الأماكن المشمسة.",
      "قم بري النباتات مرتين أسبوعيًا.",
      "استخدم سمادًا عضويًا كل شهر.",
      "قم بتقليم النباتات بانتظام.",
    ],
    tips: [
      "أضف السماد العضوي لتحسين التربة.",
      "استخدم مبيدات طبيعية للوقاية من الآفات.",
    ],
    schedule: [
      "الأسبوع الأول: تحضير التربة وزراعة الشتلات.",
      "الأسبوع الثاني: بدء الري والتسميد.",
      "الشهر الأول: مراقبة النمو وتقليم النباتات.",
    ],
    materials: [
      "أدوات الزراعة: مجرفة، معول، قفازات.",
      "مواد: تربة زراعية، سماد عضوي، بذور أو شتلات.",
    ],
  };

  useEffect(() => {
    // Simulate loading delay
    const timer = setTimeout(() => {
      setAiOutput(mockAiOutput); // Set the mock output
      setLoading(false);
    }, 1000);

    return () => clearTimeout(timer);
  }, []);

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

  return (
    <Box sx={{ p: 3 }} dir="rtl">
      <Typography variant="h4" gutterBottom color="primary">
        {aiOutput.title}
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
              {aiOutput.userInputs.map((input, index) => (
                <Typography key={index} variant="body1" sx={{ mb: 0.5 }}>
                  {input}
                </Typography>
              ))}
            </CardContent>
          </Card>
        </Grid>

        <Grid item xs={12} md={6}>
          <Card elevation={3} sx={{ borderRadius: 2 }}>
            <CardContent>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <PlantIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6" gutterBottom>
                  التوصيات الرئيسية:
                </Typography>
              </Box>
              {aiOutput.recommendations.map((recommendation, index) => (
                <Typography key={index} variant="body1" sx={{ mb: 0.5 }}>
                  {recommendation}
                </Typography>
              ))}
            </CardContent>
          </Card>
        </Grid>

        <Grid item xs={12} md={6}>
          <Card elevation={3} sx={{ borderRadius: 2 }}>
            <CardContent>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <ChecklistIcon color="primary" sx={{ mr: 1 }} />
                <Typography variant="h6" gutterBottom>
                  خطوات الزراعة:
                </Typography>
              </Box>
              {aiOutput.steps.map((step, index) => (
                <Typography key={index} variant="body1" sx={{ mb: 0.5 }}>
                  {step}
                </Typography>
              ))}
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
              {aiOutput.tips.map((tip, index) => (
                <Typography key={index} variant="body1" sx={{ mb: 0.5 }}>
                  {tip}
                </Typography>
              ))}
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
              {aiOutput.schedule.map((item, index) => (
                <Typography key={index} variant="body1" sx={{ mb: 0.5 }}>
                  {item}
                </Typography>
              ))}
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
              {aiOutput.materials.map((material, index) => (
                <Typography key={index} variant="body1" sx={{ mb: 0.5 }}>
                  {material}
                </Typography>
              ))}
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
