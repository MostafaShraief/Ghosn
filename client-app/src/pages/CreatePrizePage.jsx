import React, { useState } from "react";
import {
  Box,
  Typography,
  TextField,
  Button,
  Paper,
  Alert,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import { LocalizationProvider, DatePicker } from "@mui/x-date-pickers";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";

const CreatePrizePage = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    prizeValue: "",
    prizeDate: null,
  });
  const [errors, setErrors] = useState({});
  const [submitStatus, setSubmitStatus] = useState(null);

  const validateForm = () => {
    const newErrors = {};

    if (
      !formData.prizeValue ||
      isNaN(formData.prizeValue) ||
      formData.prizeValue <= 0
    ) {
      newErrors.prizeValue = "يرجى إدخال قيمة جائزة صالحة (رقم موجب)";
    }

    if (!formData.prizeDate) {
      newErrors.prizeDate = "يرجى اختيار تاريخ الجائزة";
    } else if (formData.prizeDate < new Date()) {
      newErrors.prizeDate = "يجب أن يكون تاريخ الجائزة في المستقبل";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleChange = (field) => (eventOrValue) => {
    const value =
      field === "prizeDate" ? eventOrValue : eventOrValue.target.value;
    setFormData((prev) => ({
      ...prev,
      [field]: value,
    }));
    // Clear error when user starts typing
    if (errors[field]) {
      setErrors((prev) => ({ ...prev, [field]: "" }));
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!validateForm()) return;

    try {
      // This is where you'd typically make an API call to your backend
      const prizeData = {
        prize_price: parseInt(formData.prizeValue),
        prize_date: formData.prizeDate.toISOString().split("T")[0],
      };

      // Simulated API call - replace with your actual API endpoint
      console.log("Submitting prize:", prizeData);
      // const response = await fetch('/api/prizes', {
      //   method: 'POST',
      //   headers: { 'Content-Type': 'application/json' },
      //   body: JSON.stringify(prizeData)
      // });

      setSubmitStatus({
        type: "success",
        message: "تم إنشاء الجائزة بنجاح! سيتم عرضها في صفحة الجوائز للعملاء",
      });

      // Reset form
      setFormData({ prizeValue: "", prizeDate: null });

      // Optional: Redirect after a delay
      setTimeout(() => navigate("/donor"), 2000);
    } catch (error) {
      setSubmitStatus({
        type: "error",
        message: "حدث خطأ أثناء إنشاء الجائزة. يرجى المحاولة مرة أخرى",
      });
    }
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDateFns}>
      <Box sx={{ p: 4, maxWidth: 600, mx: "auto" }}>
        <Typography variant="h4" gutterBottom>
          إنشاء جائزة جديدة
        </Typography>

        <Paper elevation={3} sx={{ p: 3, mt: 2 }}>
          <Box component="form" onSubmit={handleSubmit} noValidate>
            <TextField
              fullWidth
              label="قيمة الجائزة"
              value={formData.prizeValue}
              onChange={handleChange("prizeValue")}
              error={!!errors.prizeValue}
              helperText={errors.prizeValue}
              type="number"
              sx={{ mb: 3 }}
              InputProps={{ inputProps: { min: 1 } }}
            />

            <DatePicker
              label="تاريخ الجائزة"
              value={formData.prizeDate}
              onChange={handleChange("prizeDate")}
              minDate={new Date()}
              renderInput={(params) => (
                <TextField
                  {...params}
                  fullWidth
                  error={!!errors.prizeDate}
                  helperText={errors.prizeDate || "اختر تاريخ منح الجائزة"}
                  sx={{ mb: 3 }}
                />
              )}
            />

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
                إنشاء الجائزة
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
          ملاحظة: يمكن جدولة جائزة واحدة فقط لكل يوم. ستظهر الجائزة في صفحة
          الجوائز للعملاء بمجرد إنشائها.
        </Typography>
      </Box>
    </LocalizationProvider>
  );
};

export default CreatePrizePage;
