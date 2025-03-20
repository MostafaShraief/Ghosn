// client-app/src/pages/DirectSupportPage.jsx
import React, { useState, useEffect } from "react";
import {
  Box,
  Typography,
  Button,
  Paper,
  TextField,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Alert,
  Grid,
  CircularProgress,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import {
  submitSupport,
  fetchPlanSummaries,
  fetchAllFarmingTools,
} from "../services/api";

const DirectSupportPage = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    selectedPlan: "",
    selectedTool: "",
    price: "",
  });
  const [errors, setErrors] = useState({});
  const [submitStatus, setSubmitStatus] = useState(null);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [plans, setPlans] = useState([]); // Dynamic plans from API
  const [farmingTools, setFarmingTools] = useState([]); // Dynamic tools from API

  // Fetch plans and tools on component mount
  useEffect(() => {
    const loadData = async () => {
      try {
        const planSummaries = await fetchPlanSummaries();
        setPlans(
          planSummaries.map((plan) => ({
            id: plan.planID,
            name: `خطة ${plan.planID}`, // Adjust based on actual data
            isCompleted: plan.isCompleted,
          }))
        );

        const tools = await fetchAllFarmingTools();
        setFarmingTools(tools);
      } catch (error) {
        setSubmitStatus({
          type: "error",
          message: "Failed to load plans or tools. Please try again later.",
        });
      }
    };
    loadData();
  }, []);

  const validateForm = () => {
    const newErrors = {};

    if (!formData.selectedPlan) {
      newErrors.selectedPlan = "يرجى اختيار خطة لدعمها";
    }
    if (!formData.selectedTool) {
      newErrors.selectedTool = "يرجى اختيار أداة زراعية";
    }
    if (!formData.price || isNaN(formData.price) || formData.price <= 0) {
      newErrors.price = "يرجى إدخال قيمة صالحة للسعر (رقم موجب)";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleChange = (field) => (event) => {
    const value = event.target.value;
    setFormData((prev) => ({
      ...prev,
      [field]: value,
    }));
    if (errors[field]) {
      setErrors((prev) => ({ ...prev, [field]: "" }));
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!validateForm()) return;

    setIsSubmitting(true);
    setSubmitStatus(null);

    try {
      const supportData = {
        planID: parseInt(formData.selectedPlan),
        price: parseInt(formData.price),
        farmingTool: formData.selectedTool,
      };

      await submitSupport(supportData);

      setSubmitStatus({
        type: "success",
        message: "تم تقديم الدعم بنجاح!",
      });

      // Reset form
      setFormData({
        selectedPlan: "",
        selectedTool: "",
        price: "",
      });

      setTimeout(() => navigate("/donor"), 2000);
    } catch (error) {
      setSubmitStatus({
        type: "error",
        message: "حدث خطأ أثناء تقديم الدعم. يرجى المحاولة مرة أخرى",
      });
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <Box sx={{ p: 3, mx: "auto" }}>
      <Typography variant="h5" gutterBottom component="h2">
        الدعم المباشر
      </Typography>

      <Paper elevation={3} sx={{ p: 3, backgroundColor: "background.paper" }}>
        <Box component="form" onSubmit={handleSubmit} noValidate>
          <Grid container spacing={3}>
            <Grid item xs={12}>
              <FormControl fullWidth error={!!errors.selectedPlan}>
                <InputLabel>اختر خطة لدعمها</InputLabel>
                <Select
                  value={formData.selectedPlan}
                  onChange={handleChange("selectedPlan")}
                  label="اختر خطة لدعمها"
                >
                  {plans.map((plan) => (
                    <MenuItem key={plan.id} value={plan.id} dir="ltr">
                      {plan.name}
                    </MenuItem>
                  ))}
                </Select>
                {errors.selectedPlan && (
                  <Typography color="error" variant="caption">
                    {errors.selectedPlan}
                  </Typography>
                )}
              </FormControl>
            </Grid>

            <Grid item xs={12}>
              <FormControl fullWidth error={!!errors.selectedTool}>
                <InputLabel>اختر أداة زراعية</InputLabel>
                <Select
                  value={formData.selectedTool}
                  onChange={handleChange("selectedTool")}
                  label="اختر أداة زراعية"
                >
                  {farmingTools.map((tool, index) => (
                    <MenuItem key={index} value={tool}>
                      {tool}
                    </MenuItem>
                  ))}
                </Select>
                {errors.selectedTool && (
                  <Typography color="error" variant="caption">
                    {errors.selectedTool}
                  </Typography>
                )}
              </FormControl>
            </Grid>

            <Grid item xs={12}>
              <TextField
                fullWidth
                label="قيمة الدعم"
                value={formData.price}
                onChange={handleChange("price")}
                error={!!errors.price}
                helperText={errors.price}
                type="number"
                InputProps={{ inputProps: { min: 1 } }}
              />
            </Grid>

            <Grid item xs={12}>
              {submitStatus && (
                <Alert severity={submitStatus.type} sx={{ mb: 2 }}>
                  {submitStatus.message}
                </Alert>
              )}
            </Grid>

            <Grid item xs={12} sm={6}>
              <Button
                type="submit"
                variant="contained"
                size="large"
                fullWidth
                disabled={isSubmitting}
                startIcon={
                  isSubmitting && <CircularProgress size={20} color="inherit" />
                }
              >
                تقديم الدعم
              </Button>
            </Grid>

            <Grid item xs={12} sm={6}>
              <Button
                variant="outlined"
                size="large"
                onClick={() => navigate("/donor")}
                fullWidth
                disabled={isSubmitting}
              >
                إلغاء
              </Button>
            </Grid>
          </Grid>
        </Box>
      </Paper>

      <Typography
        variant="body2"
        sx={{ mt: 2, color: "text.secondary", textAlign: "center" }}
      >
        سيتم إضافة الدعم إلى الخطة المختارة مع الأداة الزراعية المحددة
      </Typography>
    </Box>
  );
};

export default DirectSupportPage;
