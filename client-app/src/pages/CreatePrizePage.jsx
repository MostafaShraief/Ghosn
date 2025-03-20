// client-app/src/pages/CreatePrizePage.jsx
import React, { useState } from "react";
import {
  Box,
  Typography,
  TextField,
  Button,
  Paper,
  Alert,
  Grid, // Import Grid
  CircularProgress, // Optional: For loading state
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
  const [isSubmitting, setIsSubmitting] = useState(false); // Optional loading state

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

    setIsSubmitting(true); // Start loading (optional)
    setSubmitStatus(null); // Clear previous status

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
      // await new Promise(resolve => setTimeout(resolve, 1500)); // Simulate API delay

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
      console.error("Error creating prize:", error); // Log the error for debugging
    } finally {
      setIsSubmitting(false); // End loading (optional)
    }
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDateFns}>
      <Box
        sx={{
          p: 3, // Reduced padding for overall box
          mx: "auto",
        }}
      >
        <Typography variant="h5" gutterBottom component="h2">
          {" "}
          {/* Changed to h5 and component for semantics */}
          إنشاء جائزة جديدة
        </Typography>

        <Paper
          elevation={3}
          sx={{
            p: 3,
            mt: 2,
            backgroundColor: "background.paper", // Ensure paper background matches theme
          }}
        >
          <Box component="form" onSubmit={handleSubmit} noValidate>
            <Grid container spacing={3}>
              {" "}
              {/* Use Grid container */}
              <Grid item xs={12}>
                {" "}
                {/* Full width for prize value */}
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
                {" "}
                {/* Full width for date picker */}
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
                {" "}
                {/* Full width for submit status */}
                {submitStatus && (
                  <Alert severity={submitStatus.type} sx={{ mb: 2 }}>
                    {submitStatus.message}
                  </Alert>
                )}
              </Grid>
              <Grid item xs={12} sm={6}>
                {" "}
                {/* Half width on small screens and up for submit button */}
                <Button
                  type="submit"
                  variant="contained"
                  size="large"
                  fullWidth // Take full width within the grid item
                  disabled={isSubmitting} // Disable during submission (optional loading state)
                  startIcon={
                    isSubmitting && (
                      <CircularProgress size={20} color="inherit" />
                    )
                  } // Optional loading icon
                >
                  إنشاء الجائزة
                </Button>
              </Grid>
              <Grid item xs={12} sm={6}>
                {" "}
                {/* Half width on small screens and up for cancel button */}
                <Button
                  variant="outlined"
                  size="large"
                  onClick={() => navigate("/donor")}
                  fullWidth // Take full width within the grid item
                  disabled={isSubmitting} // Disable during submission (optional loading state)
                >
                  إلغاء
                </Button>
              </Grid>
            </Grid>
          </Box>
        </Paper>

        <Typography
          variant="body2"
          sx={{ mt: 2, color: "text.secondary", textAlign: "center" }} // Centered text
        >
          ملاحظة: يمكن جدولة جائزة واحدة فقط لكل يوم. ستظهر الجائزة في صفحة
          الجوائز للعملاء بمجرد إنشائها.
        </Typography>
      </Box>
    </LocalizationProvider>
  );
};

export default CreatePrizePage;
