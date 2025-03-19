import React, { useState } from "react";
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
  Tabs,
  Tab,
} from "@mui/material";
import { useNavigate } from "react-router-dom";

const DirectSupportPage = () => {
  const navigate = useNavigate();
  const [tabValue, setTabValue] = useState(0);
  const [formData, setFormData] = useState({
    toolAmount: "",
    financialAmount: "",
    selectedPlan: "",
  });
  const [errors, setErrors] = useState({});
  const [submitStatus, setSubmitStatus] = useState(null);

  // Mock plan data - replace with actual data from your backend
  const plans = [
    { id: 1, name: "خطة زراعة القمح - أحمد", location: "الرياض" },
    { id: 2, name: "خطة زراعة الزيتون - محمد", location: "جدة" },
    { id: 3, name: "خطة زراعة التمور - علي", location: "المدينة" },
  ];

  const handleTabChange = (event, newValue) => {
    setTabValue(newValue);
    setErrors({});
    setSubmitStatus(null);
  };

  const validateForm = () => {
    const newErrors = {};

    if (tabValue === 0) {
      if (
        !formData.toolAmount ||
        isNaN(formData.toolAmount) ||
        formData.toolAmount <= 0
      ) {
        newErrors.toolAmount = "يرجى إدخال قيمة صالحة للأدوات (رقم موجب)";
      }
    } else {
      if (
        !formData.financialAmount ||
        isNaN(formData.financialAmount) ||
        formData.financialAmount <= 0
      ) {
        newErrors.financialAmount = "يرجى إدخال قيمة صالحة للدعم (رقم موجب)";
      }
      if (!formData.selectedPlan) {
        newErrors.selectedPlan = "يرجى اختيار خطة لدعمها";
      }
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

    try {
      if (tabValue === 0) {
        // Agricultural Tools submission
        const supportData = {
          type: "agricultural_tools",
          amount: parseInt(formData.toolAmount),
        };

        // Simulated API call - replace with your actual endpoint
        console.log("Submitting tools support:", supportData);
        // const response = await fetch('/api/support/tools', {
        //   method: 'POST',
        //   headers: { 'Content-Type': 'application/json' },
        //   body: JSON.stringify(supportData)
        // });

        setSubmitStatus({
          type: "success",
          message: "تم تقديم الدعم بالأدوات الزراعية بنجاح!",
        });
      } else {
        // Financial Support submission
        const supportData = {
          type: "financial",
          amount: parseInt(formData.financialAmount),
          planId: formData.selectedPlan,
        };

        // Simulated API call - replace with your actual endpoint
        console.log("Submitting financial support:", supportData);
        // const response = await fetch('/api/support/financial', {
        //   method: 'POST',
        //   headers: { 'Content-Type': 'application/json' },
        //   body: JSON.stringify(supportData)
        // });

        setSubmitStatus({
          type: "success",
          message: "تم تقديم الدعم المالي للخطة المختارة بنجاح!",
        });
      }

      // Reset form
      setFormData({
        toolAmount: "",
        financialAmount: "",
        selectedPlan: "",
      });

      // Optional: Redirect after a delay
      setTimeout(() => navigate("/donor"), 2000);
    } catch (error) {
      setSubmitStatus({
        type: "error",
        message: "حدث خطأ أثناء تقديم الدعم. يرجى المحاولة مرة أخرى",
      });
    }
  };

  return (
    <Box sx={{ p: 4, maxWidth: 600, mx: "auto" }}>
      <Typography variant="h4" gutterBottom>
        الدعم المباشر
      </Typography>

      <Tabs value={tabValue} onChange={handleTabChange} sx={{ mb: 3 }}>
        <Tab label="أدوات زراعية" />
        <Tab label="دعم مالي لخطة" />
      </Tabs>

      <Paper elevation={3} sx={{ p: 3 }}>
        <Box component="form" onSubmit={handleSubmit} noValidate>
          {tabValue === 0 ? (
            // Agricultural Tools Form
            <TextField
              fullWidth
              label="قيمة الدعم للأدوات الزراعية"
              value={formData.toolAmount}
              onChange={handleChange("toolAmount")}
              error={!!errors.toolAmount}
              helperText={errors.toolAmount}
              type="number"
              sx={{ mb: 3 }}
              InputProps={{ inputProps: { min: 1 } }}
            />
          ) : (
            // Financial Support Form
            <>
              <FormControl
                fullWidth
                sx={{ mb: 3 }}
                error={!!errors.selectedPlan}
              >
                <InputLabel>اختر خطة لدعمها</InputLabel>
                <Select
                  value={formData.selectedPlan}
                  onChange={handleChange("selectedPlan")}
                  label="اختر خطة لدعمها"
                >
                  {plans.map((plan) => (
                    <MenuItem key={plan.id} value={plan.id}>
                      {plan.name} - {plan.location}
                    </MenuItem>
                  ))}
                </Select>
                {errors.selectedPlan && (
                  <Typography color="error" variant="caption">
                    {errors.selectedPlan}
                  </Typography>
                )}
              </FormControl>

              <TextField
                fullWidth
                label="قيمة الدعم المالي"
                value={formData.financialAmount}
                onChange={handleChange("financialAmount")}
                error={!!errors.financialAmount}
                helperText={errors.financialAmount}
                type="number"
                sx={{ mb: 3 }}
                InputProps={{ inputProps: { min: 1 } }}
              />
            </>
          )}

          {submitStatus && (
            <Alert severity={submitStatus.type} sx={{ mb: 2 }}>
              {submitStatus.message}
            </Alert>
          )}

          <Box sx={{ display: "flex", gap: 2 }}>
            <Button
              type="submit"
              variant="contained"
              size="large"
              sx={{ flex: 1 }}
            >
              تقديم الدعم
            </Button>
            <Button
              variant="outlined"
              size="large"
              onClick={() => navigate("/donor")}
              sx={{ flex: 1 }}
            >
              إلغاء
            </Button>
          </Box>
        </Box>
      </Paper>

      <Typography variant="body2" sx={{ mt: 2, color: "text.secondary" }}>
        {tabValue === 0
          ? "سيتم استخدام الدعم لتوفير الأدوات الزراعية للمزارعين"
          : "سيتم إضافة الدعم المالي إلى الخطة المختارة وإعلام صاحب الخطة"}
      </Typography>
    </Box>
  );
};

export default DirectSupportPage;
