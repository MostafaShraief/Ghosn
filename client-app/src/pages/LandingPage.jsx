import React, { useState } from "react";
import {
  Box,
  Typography,
  Button,
  Container,
  Grid,
  Card,
  CardContent,
  Link,
  AppBar,
  Toolbar,
  IconButton,
  Stack,
  useMediaQuery,
  useTheme,
  Drawer,
  Divider,
  Avatar,
  Paper,
  Fade,
  Chip,
} from "@mui/material";
import { styled } from "@mui/system";
import logo from "@/assets/ghosn.png";
import hero from "@/assets/hero.png";
import about from "@/assets/about.png";
import WavingHandIcon from "@mui/icons-material/WavingHand";
import AgricultureIcon from "@mui/icons-material/Agriculture";
import GroupsIcon from "@mui/icons-material/Groups";
import MenuIcon from "@mui/icons-material/Menu";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import AutoFixHighIcon from "@mui/icons-material/AutoFixHigh";
import CalendarMonthIcon from "@mui/icons-material/CalendarMonth";
import EcoIcon from "@mui/icons-material/EjectOutlined";
import LocalFloristIcon from "@mui/icons-material/LocalFlorist";
import CloseIcon from "@mui/icons-material/Close";
import ArrowForwardIcon from "@mui/icons-material/ArrowForward";
import WaterDropIcon from "@mui/icons-material/WaterDrop";
import BallotIcon from "@mui/icons-material/Ballot";

// Enhanced styled components with better animations and visual design
const HeroBox = styled(Box)(({ theme }) => ({
  color: theme.palette.text.primary,
  padding: theme.spacing(14, 0, 12),
  position: "relative",
  overflow: "hidden",
  background: `linear-gradient(145deg, ${theme.palette.background.default} 0%, ${theme.palette.background.paper} 60%, ${theme.palette.primary.light}15 100%)`,
}));

const SectionBox = styled(Box)(({ theme }) => ({
  padding: theme.spacing(10, 0),
  position: "relative",
}));

const FeatureCard = styled(Card)(({ theme }) => ({
  height: "100%",
  transition: "all 0.3s cubic-bezier(0.4, 0, 0.2, 1)",
  position: "relative",
  transform: "translateY(0)",
  overflow: "hidden",
  "&:hover": {
    transform: "translateY(-8px)",
    boxShadow: theme.shadows[10],
  },
  borderRadius: theme.shape.borderRadius * 2,
  border: `1px solid ${theme.palette.divider}`,
}));

const StyledAppBar = styled(AppBar)(({ theme }) => ({
  backgroundColor: theme.palette.background.paper,
  backdropFilter: "blur(20px)",
  boxShadow: "0 1px 3px rgba(0,0,0,0.05)",
  borderBottom: `1px solid ${theme.palette.divider}`,
}));

const StepContainer = styled(Paper)(({ theme }) => ({
  padding: theme.spacing(4),
  borderRadius: theme.shape.borderRadius * 2,
  height: "100%",
  boxShadow: "0 4px 20px rgba(0,0,0,0.05)",
  border: `1px solid ${theme.palette.divider}`,
  position: "relative",
  overflow: "hidden",
  "&::after": {
    content: '""',
    position: "absolute",
    top: 0,
    left: 0,
    right: 0,
    height: "4px",
    background: `linear-gradient(90deg, ${theme.palette.primary.main}, ${theme.palette.secondary.main})`,
  },
}));

const StepNumber = styled(Box)(({ theme }) => ({
  width: "50px",
  height: "50px",
  borderRadius: "50%",
  backgroundColor: theme.palette.primary.main,
  color: theme.palette.common.white,
  display: "flex",
  alignItems: "center",
  justifyContent: "center",
  fontWeight: "bold",
  fontSize: "1.5rem",
  marginRight: theme.spacing(3),
  boxShadow: "0 4px 10px rgba(0,0,0,0.15)",
  flexShrink: 0,
}));

const NavLink = styled(Link)(({ theme }) => ({
  textDecoration: "none",
  fontWeight: "600",
  color: theme.palette.text.secondary,
  position: "relative",
  padding: theme.spacing(0.5, 1),
  transition: "all 0.2s ease",
  "&:hover": {
    color: theme.palette.primary.main,
  },
  "&::after": {
    content: '""',
    position: "absolute",
    width: "0%",
    height: "2px",
    bottom: 0,
    left: 0,
    backgroundColor: theme.palette.primary.main,
    transition: "width 0.3s ease",
  },
  "&:hover::after": {
    width: "100%",
  },
}));

const GradientText = styled(Typography)(({ theme }) => ({
  background: `linear-gradient(90deg, ${theme.palette.primary.main}, ${theme.palette.secondary.main})`,
  WebkitBackgroundClip: "text",
  WebkitTextFillColor: "transparent",
  fontWeight: 700,
}));

const FloatingImage = styled(Box)(({ theme }) => ({
  borderRadius: theme.shape.borderRadius * 5,
  overflow: "hidden",
  boxShadow: "0 20px 40px rgba(0,0,0,0.1)",
  position: "relative",
  transform: "perspective(1000px) rotateY(-5deg) rotateX(5deg)",
  transition: "transform 0.5s ease",
  "&:hover": {
    transform: "perspective(1000px) rotateY(0deg) rotateX(0deg)",
  },
}));

const InlineFeatureIcon = styled(Box)(({ theme }) => ({
  width: "48px",
  height: "48px",
  borderRadius: "12px",
  display: "flex",
  alignItems: "center",
  justifyContent: "center",
  backgroundColor: theme.palette.primary.light,
  color: "white",
  marginRight: theme.spacing(2),
  boxShadow: "0 4px 8px rgba(0,0,0,0.05)",
}));

const PatternBackground = styled(Box)(({ theme }) => ({
  position: "absolute",
  top: 0,
  left: 0,
  right: 0,
  bottom: 0,
  opacity: 0.03,
  backgroundImage: `url("data:image/svg+xml,%3Csvg width='60' height='60' viewBox='0 0 60 60' xmlns='http://www.w3.org/2000/svg'%3E%3Cg fill='none' fill-rule='evenodd'%3E%3Cg fill='%23${theme.palette.primary.main.replace(
    "#",
    ""
  )}' fill-opacity='1'%3E%3Cpath d='M36 34v-4h-2v4h-4v2h4v4h2v-4h4v-2h-4zm0-30V0h-2v4h-4v2h4v4h2V6h4V4h-4zM6 34v-4H4v4H0v2h4v4h2v-4h4v-2H6zM6 4V0H4v4H0v2h4v4h2V6h4V4H6z'/%3E%3C/g%3E%3C/g%3E%3C/svg%3E")`,
  zIndex: -1,
}));

const ScrollGradient = styled(Box)(({ theme }) => ({
  position: "absolute",
  bottom: 0,
  left: 0,
  width: "100%",
  height: "100px",
  background: `linear-gradient(to top, ${theme.palette.background.default}, transparent)`,
  zIndex: -1,
}));

const TeamCard = styled(Card)(({ theme }) => ({
  height: "100%",
  transition: "all 0.3s cubic-bezier(0.4, 0, 0.2, 1)",
  "&:hover": {
    transform: "scale(1.05)",
    boxShadow: theme.shadows[8],
  },
  borderRadius: theme.shape.borderRadius * 2,
  border: `1px solid ${theme.palette.divider}`,
  overflow: "hidden",
}));

function LandingPage() {
  const [mobileOpen, setMobileOpen] = useState(false);
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down("sm"));
  const isMedium = useMediaQuery(theme.breakpoints.down("md"));

  const handleDrawerToggle = () => {
    setMobileOpen(!mobileOpen);
  };

  const features = [
    {
      icon: <AgricultureIcon fontSize="large" />,
      title: "تخطيط الزراعة",
      description:
        "احصل على خطة زراعية مخصصة بناءً على مساحتك، مناخك، ونوع التربة لديك.",
    },
    {
      icon: <GroupsIcon fontSize="large" />,
      title: "دعم الخبراء",
      description:
        'تواصل مع مساعد الذكاء الاصطناعي "غصن" لطرح الأسئلة والحصول على إرشادات فورية.',
    },
    {
      icon: <CalendarMonthIcon fontSize="large" />,
      title: "متابعة دورية",
      description: "استقبل إشعارات وتذكيرات للعناية بنباتاتك في الوقت المناسب.",
    },
    {
      icon: <WaterDropIcon fontSize="large" />,
      title: "نصائح الري",
      description:
        "تعرف على كمية المياه المناسبة وأوقات الري المثالية لكل نوع من النباتات.",
    },
    {
      icon: <LocalFloristIcon fontSize="large" />,
      title: "اختيار النباتات",
      description:
        "اكتشف أفضل النباتات المناسبة لمساحتك ومناخك وأهدافك الزراعية.",
    },
    {
      icon: <EcoIcon fontSize="large" />,
      title: "زراعة صديقة للبيئة",
      description: "تعلم أساليب الزراعة المستدامة والعضوية للحفاظ على البيئة.",
    },
  ];

  const howItWorksSteps = [
    {
      number: 1,
      title: "املأ الاستمارة",
      description: "أدخل معلومات حول مساحتك الزراعية، مناخك، ونوع التربة.",
    },
    {
      number: 2,
      title: "احصل على خطة مخصصة",
      description: "يقوم غصن بإنشاء خطة زراعية مفصلة تناسب احتياجاتك.",
    },
    {
      number: 3,
      title: "ابدأ الزراعة",
      description: "اتبع الخطة واستمتع بمشاهدة نباتاتك تنمو وتزدهر!",
    },
  ];

  const teamMembers = [
    {
      name: "ابوبكر",
      role: "مطور",
      description: "عنوان يشرح ماذا فعل الشخص",
      avatar: "https://placehold.co/100x100?text=Abubakr",
    },
    {
      name: "مصطفى",
      role: "مطور",
      description: "عنوان يشرح ماذا فعل الشخص",
      avatar: "https://placehold.co/100x100?text=Mostafa",
    },
    {
      name: "احمد",
      role: "مطور",
      description: "عنوان يشرح ماذا فعل الشخص",
      avatar: "https://placehold.co/100x100?text=Ahmad",
    },
    {
      name: "سيف",
      role: "مطور",
      description: "عنوان يشرح ماذا فعل الشخص",
      avatar: "https://placehold.co/100x100?text=Seif",
    },
  ];

  return (
    <Box dir="rtl">
      {/* Enhanced Navbar */}
      <StyledAppBar position="sticky">
        <Container maxWidth="lg">
          <Toolbar sx={{ px: { xs: 0 } }}>
            <IconButton
              color="inherit"
              aria-label="open drawer"
              edge="start"
              onClick={handleDrawerToggle}
              sx={{ mr: 2, display: { md: "none" } }}
            >
              <MenuIcon />
            </IconButton>
            <Typography
              variant="h6"
              component="div"
              sx={{ color: "primary.main", fontWeight: "bold" }}
            >
              <Link
                href="#"
                sx={{
                  textDecoration: "none",
                  color: "primary.main",
                  display: "flex",
                  alignItems: "center",
                }}
              >
                <img src={logo} alt="Ghosn Logo" height="55" />
              </Link>
            </Typography>
            <Box
              sx={{
                display: {
                  xs: "none",
                  flexGrow: 1,
                  justifyContent: "center",
                  md: "flex",
                  fontSize: "1.2rem",
                },
                ml: 3,
              }}
            >
              <NavLink href="#">الرئيسية</NavLink>
              <NavLink href="#about" sx={{ mx: 2 }}>
                عن المشروع
              </NavLink>
              <NavLink href="#how-it-works" sx={{ mx: 2 }}>
                كيف يعمل
              </NavLink>
              <NavLink href="#who-we-are" sx={{ mx: 2 }}>
                من نحن
              </NavLink>
            </Box>
            <Stack direction="row" spacing={1}>
              <Button
                variant="contained"
                color="primary"
                size={isMobile ? "small" : "medium"}
                href="/app"
                disableElevation
                sx={{
                  borderRadius: "8px",
                  px: 2,
                  fontWeight: "600",
                }}
              >
                العملاء
              </Button>
              <Button
                variant="outlined"
                color="primary"
                size={isMobile ? "small" : "medium"}
                href="/donor"
                sx={{
                  borderRadius: "8px",
                  px: 2,
                  fontWeight: "600",
                }}
              >
                المتبرعين
              </Button>
            </Stack>
          </Toolbar>
        </Container>
      </StyledAppBar>

      {/* Mobile Menu as a proper Drawer */}
      <Drawer
        variant="temporary"
        open={mobileOpen}
        onClose={handleDrawerToggle}
        ModalProps={{
          keepMounted: true, // Better mobile performance
        }}
        sx={{
          display: { xs: "block", md: "none" },
          "& .MuiDrawer-paper": {
            boxSizing: "border-box",
            width: 280,
            borderRight: `1px solid ${theme.palette.divider}`,
          },
        }}
      >
        <Box
          sx={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
            p: 2,
          }}
        >
          <Box sx={{ display: "flex", alignItems: "center" }}>
            <img src={logo} alt="Ghosn Logo" height="30" />
          </Box>
          <IconButton onClick={handleDrawerToggle}>
            <CloseIcon />
          </IconButton>
        </Box>
        <Divider />
        <Box sx={{ p: 2 }}>
          <Link
            href="#"
            onClick={handleDrawerToggle}
            sx={{
              display: "block",
              mb: 2,
              textDecoration: "none",
              fontWeight: "bold",
              color: "text.primary",
              py: 1,
            }}
          >
            الرئيسية
          </Link>
          <Link
            href="#about"
            onClick={handleDrawerToggle}
            sx={{
              display: "block",
              mb: 2,
              textDecoration: "none",
              fontWeight: "bold",
              color: "text.primary",
              py: 1,
            }}
          >
            عن المشروع
          </Link>
          <Link
            href="#how-it-works"
            onClick={handleDrawerToggle}
            sx={{
              display: "block",
              mb: 2,
              textDecoration: "none",
              fontWeight: "bold",
              color: "text.primary",
              py: 1,
            }}
          >
            كيف يعمل
          </Link>
          <Link
            href="#who-we-are"
            onClick={handleDrawerToggle}
            sx={{
              display: "block",
              mb: 2,
              textDecoration: "none",
              fontWeight: "bold",
              color: "text.primary",
              py: 1,
            }}
          >
            من نحن
          </Link>
        </Box>
        <Divider />
        <Box sx={{ p: 2 }}>
          <Button
            variant="contained"
            color="primary"
            fullWidth
            href="/app/login"
            sx={{ mb: 1 }}
          >
            العملاء
          </Button>
          <Button variant="outlined" color="primary" fullWidth>
            المتبرعين
          </Button>
        </Box>
      </Drawer>

      {/* Enhanced Hero Section */}
      <HeroBox>
        <PatternBackground />
        <Container maxWidth="lg">
          <Grid
            container
            spacing={5}
            alignItems="center"
            justifyContent="center"
          >
            <Grid item xs={12} md={6}>
              <Fade in={true} timeout={1000}>
                <Box>
                  <Box sx={{ mb: 1 }}>
                    <Chip
                      label="منصة زراعية ذكية"
                      color="primary"
                      size="small"
                      sx={{ fontWeight: "bold", mb: 2 }}
                    />
                  </Box>
                  <Stack
                    direction="row"
                    spacing={1}
                    alignItems="center"
                    sx={{ mb: 1 }}
                  >
                    <GradientText
                      variant={isMobile ? "h3" : "h2"}
                      component="h1"
                      fontWeight="bold"
                    >
                      مرحبًا بك في غصن
                    </GradientText>
                    <WavingHandIcon fontSize="large" color="secondary" />
                  </Stack>
                  <Typography
                    variant="h6"
                    component="p"
                    paragraph
                    sx={{
                      fontWeight: "medium",
                      color: "text.secondary",
                      mb: 4,
                      lineHeight: 1.6,
                    }}
                  >
                    دليلك الشامل لزراعة ناجحة، من البذرة إلى الثمرة. نقدم لك
                    الأدوات والنصائح لتحويل أي مساحة إلى واحة خضراء.
                  </Typography>
                  <Stack
                    direction={{ xs: "column", sm: "row" }}
                    spacing={2}
                    mt={4}
                  >
                    <Button
                      variant="contained"
                      color="primary"
                      size="large"
                      href="/app/planting-form"
                      disableElevation
                      sx={{
                        borderRadius: "10px",
                        px: 4,
                        py: 1.5,
                        fontWeight: "600",
                        boxShadow: "0 10px 20px rgba(0,0,0,0.1)",
                      }}
                      endIcon={<ArrowForwardIcon />}
                    >
                      ابدأ الزراعة الآن
                    </Button>
                    <Button
                      variant="outlined"
                      color="primary"
                      size="large"
                      href="/app/chat"
                      sx={{
                        borderRadius: "10px",
                        px: 4,
                        py: 1.5,
                        fontWeight: "600",
                      }}
                    >
                      اسأل غصن
                    </Button>
                  </Stack>
                </Box>
              </Fade>
            </Grid>
            <Grid item xs={12} md={6}>
              <Fade in={true} timeout={1500}>
                <Box>
                  <FloatingImage>
                    <img
                      src={hero}
                      alt="Ghosn Illustration"
                      style={{ width: "100%", display: "block" }}
                    />
                    <Box
                      sx={{
                        position: "absolute",
                        top: 0,
                        left: 0,
                        width: "100%",
                        height: "100%",
                        background: `linear-gradient(135deg, rgba(67, 160, 71, 0.2) 0%, rgba(255, 255, 255, 0) 70%)`,
                        pointerEvents: "none",
                      }}
                    />
                  </FloatingImage>
                </Box>
              </Fade>
            </Grid>
          </Grid>
        </Container>
      </HeroBox>

      {/* Enhanced Features Section */}
      <SectionBox>
        <Container maxWidth="lg">
          <Box sx={{ textAlign: "center", mb: 8 }}>
            <Typography
              variant="overline"
              component="p"
              color="primary"
              fontWeight="bold"
              gutterBottom
            >
              ما يميزنا
            </Typography>
            <Typography
              variant="h3"
              component="h2"
              gutterBottom
              fontWeight="bold"
            >
              مميزات غصن
            </Typography>
            <Typography
              variant="body1"
              color="text.secondary"
              sx={{ maxWidth: 650, mx: "auto" }}
            >
              نقدم مجموعة من الأدوات والخدمات المتكاملة لمساعدتك في رحلة الزراعة
            </Typography>
          </Box>

          <Grid container spacing={3} justifyContent="center">
            {features.map((feature, index) => (
              <Grid item xs={12} sm={6} md={4} key={index}>
                <FeatureCard>
                  <CardContent sx={{ p: 3, height: "100%" }}>
                    <Stack
                      direction="row"
                      spacing={2}
                      alignItems="center"
                      sx={{ mb: 2 }}
                    >
                      <InlineFeatureIcon>{feature.icon}</InlineFeatureIcon>
                      <Typography
                        variant="h6"
                        component="h3"
                        gutterBottom
                        fontWeight="bold"
                      >
                        {feature.title}
                      </Typography>
                    </Stack>
                    <Typography variant="body1" color="text.secondary">
                      {feature.description}
                    </Typography>
                  </CardContent>
                </FeatureCard>
              </Grid>
            ))}
          </Grid>
        </Container>
      </SectionBox>

      {/* Enhanced About Section */}
      <SectionBox sx={{ bgcolor: "background.paper" }} id="about">
        <PatternBackground />
        <Container maxWidth="lg">
          <Grid container spacing={6} alignItems="center">
            <Grid item xs={12} md={6}>
              <Fade in={true} timeout={1000}>
                <Box>
                  <Typography
                    variant="overline"
                    component="p"
                    color="primary"
                    fontWeight="bold"
                    gutterBottom
                  >
                    تعرف علينا
                  </Typography>
                  <Typography
                    variant="h3"
                    component="h2"
                    gutterBottom
                    fontWeight="bold"
                  >
                    عن غصن
                  </Typography>
                  <Typography
                    variant="body1"
                    paragraph
                    color="text.secondary"
                    sx={{ mb: 3, lineHeight: 1.8 }}
                  >
                    غصن هو مشروع يهدف إلى تسهيل عملية الزراعة للجميع، سواء كانوا
                    مزارعين متمرسين أو مبتدئين. نحن نؤمن بأن الزراعة يمكن أن
                    تكون ممتعة ومجزية، ونسعى لتوفير الأدوات اللازمة لتحقيق ذلك.
                  </Typography>
                  <Typography
                    variant="body1"
                    paragraph
                    color="text.secondary"
                    sx={{ lineHeight: 1.8 }}
                  >
                    من خلال الذكاء الاصطناعي والخبرة الزراعية، نقدم حلولًا مخصصة
                    تناسب احتياجاتك وظروفك البيئية المحلية، مما يضمن نجاح مشروعك
                    الزراعي بغض النظر عن مستوى خبرتك.
                  </Typography>

                  <Grid container spacing={3} sx={{ mt: 2 }}>
                    <Grid item xs={6}>
                      <Box
                        sx={{ display: "flex", alignItems: "center", mb: 1 }}
                      >
                        <CheckCircleIcon color="primary" sx={{ mr: 1 }} />
                        <Typography variant="body1" fontWeight="medium">
                          خطط زراعية مخصصة
                        </Typography>
                      </Box>
                      <Box sx={{ display: "flex", alignItems: "center" }}>
                        <CheckCircleIcon color="primary" sx={{ mr: 1 }} />
                        <Typography variant="body1" fontWeight="medium">
                          نصائح موسمية
                        </Typography>
                      </Box>
                    </Grid>
                    <Grid item xs={6}>
                      <Box
                        sx={{ display: "flex", alignItems: "center", mb: 1 }}
                      >
                        <CheckCircleIcon color="primary" sx={{ mr: 1 }} />
                        <Typography variant="body1" fontWeight="medium">
                          تذكيرات آلية
                        </Typography>
                      </Box>
                      <Box sx={{ display: "flex", alignItems: "center" }}>
                        <CheckCircleIcon color="primary" sx={{ mr: 1 }} />
                        <Typography variant="body1" fontWeight="medium">
                          دعم مستمر
                        </Typography>
                      </Box>
                    </Grid>
                  </Grid>

                  <Button
                    variant="outlined"
                    color="primary"
                    size="large"
                    sx={{ mt: 4, borderRadius: "8px", px: 3, py: 1 }}
                  >
                    اكتشف المزيد
                  </Button>
                </Box>
              </Fade>
            </Grid>
            <Grid item xs={12} md={6}>
              <Fade in={true} timeout={1000}>
                <Box
                  sx={{
                    position: "relative",
                    borderRadius: "24px",
                    overflow: "hidden",
                    height: { xs: "300px", md: "400px" },
                    boxShadow: "0 20px 40px rgba(0,0,0,0.1)",
                  }}
                >
                  <img
                    src={about}
                    alt="About Ghosn"
                    style={{
                      width: "100%",
                      height: "100%",
                      objectFit: "cover",
                    }}
                  />
                  <Box
                    sx={{
                      position: "absolute",
                      bottom: 0,
                      left: 0,
                      right: 0,
                      p: 3,
                      background:
                        "linear-gradient(to top, rgba(0,0,0,0.7), transparent)",
                      color: "white",
                    }}
                  >
                    <Typography variant="h6" fontWeight="bold">
                      استمتع بتجربة زراعية ناجحة
                    </Typography>
                  </Box>
                </Box>
              </Fade>
            </Grid>
          </Grid>
        </Container>
      </SectionBox>

      {/* Enhanced How It Works Section */}
      <SectionBox id="how-it-works">
        <Container maxWidth="lg">
          <Box sx={{ textAlign: "center", mb: 8 }}>
            <Typography
              variant="overline"
              component="p"
              color="primary"
              fontWeight="bold"
              gutterBottom
            >
              خطوات بسيطة
            </Typography>
            <Typography
              variant="h3"
              component="h2"
              gutterBottom
              fontWeight="bold"
            >
              كيف يعمل غصن
            </Typography>
            <Typography
              variant="body1"
              color="text.secondary"
              sx={{ maxWidth: 650, mx: "auto" }}
            >
              اتبع هذه الخطوات البسيطة للحصول على تجربة زراعية ناجحة مع غصن
            </Typography>
          </Box>

          <Grid container spacing={4}>
            {howItWorksSteps.map((step, index) => (
              <Grid item xs={12} md={4} key={index}>
                <Fade in={true} timeout={1000 + index * 300}>
                  <StepContainer elevation={0}>
                    <Box
                      sx={{
                        display: "flex",
                        flexDirection: "column",
                        height: "100%",
                      }}
                    >
                      <Box
                        sx={{ display: "flex", alignItems: "center", mb: 3 }}
                      >
                        <StepNumber>{step.number}</StepNumber>
                        <Typography
                          variant="h5"
                          component="h3"
                          fontWeight="bold"
                        >
                          {step.title}
                        </Typography>
                      </Box>
                      <Typography
                        variant="body1"
                        color="text.secondary"
                        sx={{ lineHeight: 1.7 }}
                      >
                        {step.description}
                      </Typography>

                      {step.number === 1 && (
                        <Box sx={{ mt: "auto", pt: 3 }}>
                          <BallotIcon color="primary" sx={{ fontSize: 40 }} />
                        </Box>
                      )}
                      {step.number === 2 && (
                        <Box sx={{ mt: "auto", pt: 3 }}>
                          <AutoFixHighIcon
                            color="primary"
                            sx={{ fontSize: 40 }}
                          />
                        </Box>
                      )}
                      {step.number === 3 && (
                        <Box sx={{ mt: "auto", pt: 3 }}>
                          <LocalFloristIcon
                            color="primary"
                            sx={{ fontSize: 40 }}
                          />
                        </Box>
                      )}
                    </Box>
                  </StepContainer>
                </Fade>
              </Grid>
            ))}
          </Grid>

          <Box sx={{ textAlign: "center", mt: 6 }}>
            <Button
              variant="contained"
              color="primary"
              size="large"
              href="/app/planting-form"
              disableElevation
              sx={{
                borderRadius: "10px",
                px: 4,
                py: 1.5,
                fontWeight: "600",
                boxShadow: "0 10px 20px rgba(0,0,0,0.1)",
              }}
              endIcon={<ArrowForwardIcon />}
            >
              جرب غصن الآن
            </Button>
          </Box>
        </Container>
      </SectionBox>

      <SectionBox id="who-we-are" sx={{ bgcolor: "background.paper" }}>
        <PatternBackground />
        <Container maxWidth="lg">
          <Box sx={{ textAlign: "center", mb: 8 }}>
            <Typography
              variant="overline"
              component="p"
              color="primary"
              fontWeight="bold"
              gutterBottom
            >
              فريقنا
            </Typography>
            <Typography
              variant="h3"
              component="h2"
              gutterBottom
              fontWeight="bold"
            >
              من نحن
            </Typography>
            <Typography
              variant="body1"
              color="text.secondary"
              sx={{ maxWidth: 650, mx: "auto" }}
            >
              تعرف على الفريق الذي يقف وراء غصن، ملتزمون بجعل الزراعة أسهل وأكثر
              استدامة للجميع.
            </Typography>
          </Box>

          <Grid container spacing={4} justifyContent="center">
            {teamMembers.map((member, index) => (
              <Grid item xs={12} sm={6} md={3} key={index}>
                <TeamCard>
                  <CardContent sx={{ p: 3, textAlign: "center" }}>
                    <Avatar
                      src={member.avatar}
                      alt={member.name}
                      sx={{
                        width: 100,
                        height: 100,
                        mx: "auto",
                        mb: 2,
                        border: `3px solid ${theme.palette.primary.main}`,
                        boxShadow: "0 4px 12px rgba(0,0,0,0.1)",
                      }}
                    />
                    <Typography variant="h6" fontWeight="bold" gutterBottom>
                      {member.name}
                    </Typography>
                    <Typography
                      variant="body2"
                      color="primary"
                      fontWeight="medium"
                      gutterBottom
                    >
                      {member.role}
                    </Typography>
                    <Typography variant="body1" color="text.secondary">
                      {member.description}
                    </Typography>
                  </CardContent>
                </TeamCard>
              </Grid>
            ))}
          </Grid>

          <Box sx={{ textAlign: "center", mt: 6 }}>
            <Button
              variant="outlined"
              color="primary"
              size="large"
              href="/app/contact"
              sx={{ borderRadius: "8px", px: 4, py: 1.5 }}
            >
              تواصل مع فريقنا
            </Button>
          </Box>
        </Container>
      </SectionBox>

      {/* Footer Section */}
      <Box
        sx={{
          bgcolor: "background.paper",
          py: 2,
        }}
      >
        <Container maxWidth="lg">
          <Grid container spacing={4}>
            <Grid item xs={12} md={4}>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <img src={logo} alt="Ghosn Logo" height="40" />
              </Box>
              <Typography variant="body2" color="text.secondary" paragraph>
                منصة زراعية ذكية تساعدك على تحويل أي مساحة إلى حديقة مزدهرة
                باستخدام الذكاء الاصطناعي والخبرة الزراعية.
              </Typography>
            </Grid>

            <Grid item xs={12} md={2}>
              <Typography variant="h6" fontWeight="bold" gutterBottom>
                الروابط
              </Typography>
              <Stack spacing={1}>
                <NavLink href="#" underline="none">
                  الرئيسية
                </NavLink>
                <NavLink href="#about" underline="none">
                  عن المشروع
                </NavLink>
                <NavLink href="#how-it-works" underline="none">
                  كيف يعمل
                </NavLink>
                <NavLink href="#who-we-are" underline="none">
                  من نحن
                </NavLink>
              </Stack>
            </Grid>

            <Grid item xs={12} md={3}>
              <Typography variant="h6" fontWeight="bold" gutterBottom>
                تواصل معنا
              </Typography>
              <Typography variant="body2" color="text.secondary" paragraph>
                info@ghosn.com
              </Typography>
              <Button
                variant="contained"
                color="primary"
                sx={{ borderRadius: "8px", px: 3 }}
                href="/app/contact"
              >
                راسلنا
              </Button>
            </Grid>
          </Grid>

          <Box sx={{ textAlign: "center", mt: 2 }}>
            <Typography variant="body2" color="text.secondary">
              © {new Date().getFullYear()} غصن. جميع الحقوق محفوظة.
            </Typography>
          </Box>
        </Container>
      </Box>
    </Box>
  );
}

export default LandingPage;
