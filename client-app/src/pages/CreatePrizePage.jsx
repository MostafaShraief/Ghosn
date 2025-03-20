import React, { useState } from "react";
import {
  Box,
  Typography,
  TextField,
  Button,
  Paper,
  Alert,
  Grid,
  CircularProgress,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import { LocalizationProvider, DatePicker } from "@mui/x-date-pickers";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import api from "../services/api"; // Import the API client

const CreatePrizePage = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    prizeValue: "",
    prizeDate: null,
  });
  const [errors, setErrors] = useState({});
  const [submitStatus, setSubmitStatus] = useState(null);
  const [isSubmitting, setIsSubmitting] = useState(false);

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
      //Corrected date comparision.  Need to strip the time portion for correct future-date check.
      const today = new Date();
      today.setHours(0, 0, 0, 0); // Set time to 00:00:00 for accurate date-only comparison
      const selectedDate = new Date(formData.prizeDate);
      selectedDate.setHours(0, 0, 0, 0);
      if (selectedDate < today) {
        newErrors.prizeDate = "يجب أن يكون تاريخ الجائزة في المستقبل";
      }
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

    setIsSubmitting(true);
    setSubmitStatus(null);

    try {
      const prizeData = {
        prizeMoney: parseInt(formData.prizeValue), // Corrected field name
        date: formData.prizeDate.toISOString(), // Corrected format -  full ISO string for the API.
      };

      const response = await api.post("/api/Ghosn/Prizes/Add", prizeData); // Use the imported API client

      if (response.status === 200 || response.status === 201) {
        // Check for successful status codes (200 OK or 201 Created)
        setSubmitStatus({
          type: "success",
          message: "تم إنشاء الجائزة بنجاح! سيتم عرضها في صفحة الجوائز للعملاء",
        });

        setFormData({ prizeValue: "", prizeDate: null });
        setTimeout(() => navigate("/donor"), 2000);
      } else {
        // Handle non-successful responses (e.g., 400, 500)
        setSubmitStatus({
          type: "error",
          message: `حدث خطأ أثناء إنشاء الجائزة.  رمز الحالة: ${response.status} - ${response.data}`,
        });
        console.error(
          "Error creating prize: Status",
          response.status,
          response.data
        );
      }
    } catch (error) {
      // Handle network errors or errors thrown by axios
      console.error("Error creating prize:", error);

      let errorMessage = "حدث خطأ أثناء إنشاء الجائزة. يرجى المحاولة مرة أخرى";

      // Check for different types of errors and provide more specific messages if possible
      if (error.response) {
        // The request was made and the server responded with a status code
        // that falls out of the range of 2xx
        console.error("Response data:", error.response.data);
        console.error("Response status:", error.response.status);
        console.error("Response headers:", error.response.headers);
        errorMessage = `حدث خطأ من الخادم: ${error.response.status} - ${
          error.response.data.details || "تفاصيل غير متوفرة"
        }`; // Try to use server-provided error message if available.
      } else if (error.request) {
        // The request was made but no response was received
        console.error("No response received:", error.request);
        errorMessage =
          "لا يوجد رد من الخادم.  يرجى التحقق من اتصالك بالإنترنت.";
      } else {
        // Something happened in setting up the request that triggered an Error
        console.error("Request setup error:", error.message);
        errorMessage = "خطأ في إعداد الطلب: " + error.message;
      }

      setSubmitStatus({ type: "error", message: errorMessage });
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDateFns}>
      <Box
        sx={{
          p: 3,
          mx: "auto",
        }}
      >
        <Typography variant="h5" gutterBottom component="h2">
          إنشاء جائزة جديدة
        </Typography>

        <Paper
          elevation={3}
          sx={{
            p: 3,
            mt: 2,
            backgroundColor: "background.paper",
          }}
        >
          <Box component="form" onSubmit={handleSubmit} noValidate>
            <Grid container spacing={3}>
              <Grid item xs={12}>
                <TextField
                  fullWidth
                  label="قيمة الجائزة"
                  value={formData.prizeValue}
                  onChange={handleChange("prizeValue")}
                  error={!!errors.prizeValue}
                  helperText={errors.prizeValue}
                  type="number"
                  InputProps={{ inputProps: { min: 1 } }}
                />
              </Grid>
              <Grid item xs={12}>
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
                    />
                  )}
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
                    isSubmitting && (
                      <CircularProgress size={20} color="inherit" />
                    )
                  }
                >
                  إنشاء الجائزة
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
          ملاحظة: يمكن جدولة جائزة واحدة فقط لكل يوم. ستظهر الجائزة في صفحة
          الجوائز للعملاء بمجرد إنشائها.
        </Typography>
      </Box>
    </LocalizationProvider>
  );
};

export default CreatePrizePage;
