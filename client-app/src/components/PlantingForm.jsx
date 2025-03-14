import React, { useState } from "react";
import {
  FormControlLabel,
  Checkbox,
  TextField,
  Button,
  Typography,
  Grid,
  Paper,
  Box,
  FormControl,
  FormLabel,
  RadioGroup,
  Radio,
} from "@mui/material";
import { styled } from "@mui/material/styles";
import axios from "axios";

// Styled Paper component
const FormPaper = styled(Paper)(({ theme }) => ({
  padding: theme.spacing(4),
  margin: theme.spacing(4, "auto"),
  maxWidth: 800,
  borderRadius: 16,
  boxShadow: "0 8px 32px rgba(0, 0, 0, 0.1)",
}));

const PlantingForm = () => {
  const [locationTypes, setLocationTypes] = useState([]);
  const [areaDimensions, setAreaDimensions] = useState({
    length: "",
    width: "",
  });
  const [climate, setClimate] = useState([]);
  const [temperature, setTemperature] = useState([]);
  const [soilType, setSoilType] = useState([]);
  const [soilFertility, setSoilFertility] = useState("");
  const [currentPlants, setCurrentPlants] = useState({
    hasPlants: null,
    types: [],
  });
  const [plantHealth, setPlantHealth] = useState("");
  const [pesticide, setPesticide] = useState("");

  const handleCheckboxChange = (setState, value) => {
    setState((prev) =>
      prev.includes(value)
        ? prev.filter((item) => item !== value)
        : [...prev, value]
    );
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    const formData = {
      locationTypes,
      areaDimensions,
      climate,
      temperature,
      soilType,
      soilFertility,
      currentPlants,
      plantHealth,
      pesticide,
    };

    axios
      .post("https://your-api-endpoint.com/submit", formData)
      .then((response) => {
        console.log("تم إرسال البيانات بنجاح:", response.data);
      })
      .catch((error) => {
        console.error("حدث خطأ أثناء إرسال البيانات:", error);
      });
  };

  return (
    <FormPaper elevation={3} sx={{ maxWidth: "100%", margin: 0 }} dir="rtl">
      <Typography
        variant="h4"
        component="h1"
        gutterBottom
        color="primary"
        sx={{ mb: 4 }}
      >
        نموذج معلومات الزراعة
      </Typography>
      <form onSubmit={handleSubmit}>
        <Grid container spacing={3}>
          {/* Location Type */}
          <Grid item xs={12}>
            <FormControl component="fieldset">
              <FormLabel component="legend" required sx={{ mb: 1 }}>
                نوع مكان الزراعة
              </FormLabel>
              <Box sx={{ display: "flex", flexWrap: "wrap", gap: 2 }}>
                {["حقل مفتوح", "حديقة منزلية", "سقف", "أوعية", "بيت زجاجي"].map(
                  (option) => (
                    <FormControlLabel
                      key={option}
                      control={
                        <Checkbox
                          checked={locationTypes.includes(option)}
                          onChange={() =>
                            handleCheckboxChange(setLocationTypes, option)
                          }
                          color="primary"
                        />
                      }
                      label={option}
                    />
                  )
                )}
              </Box>
            </FormControl>
          </Grid>

          {/* Area Dimensions */}
          <Grid item xs={12}>
            <FormLabel component="legend" required sx={{ mb: 1 }}>
              الطول والعرض
            </FormLabel>
            <Grid container spacing={2}>
              <Grid item xs={12} sm={6}>
                <TextField
                  dir="rtl"
                  label="طول المنطقة (متر)"
                  value={areaDimensions.length}
                  onChange={(e) =>
                    setAreaDimensions({
                      ...areaDimensions,
                      length: e.target.value,
                    })
                  }
                  fullWidth
                  variant="outlined"
                  type="number"
                  InputProps={{ inputProps: { min: 0 } }}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  label="عرض المنطقة (متر)"
                  value={areaDimensions.width}
                  onChange={(e) =>
                    setAreaDimensions({
                      ...areaDimensions,
                      width: e.target.value,
                    })
                  }
                  fullWidth
                  variant="outlined"
                  type="number"
                  InputProps={{ inputProps: { min: 0 } }}
                />
              </Grid>
            </Grid>
          </Grid>

          {/* Climate */}
          <Grid item xs={12}>
            <FormControl component="fieldset">
              <FormLabel component="legend" required sx={{ mb: 1 }}>
                المناخ
              </FormLabel>
              <Box sx={{ display: "flex", flexWrap: "wrap", gap: 2 }}>
                {["حار وجاف", "معتدل", "بارد", "رطب", "متغير"].map((option) => (
                  <FormControlLabel
                    key={option}
                    control={
                      <Checkbox
                        checked={climate.includes(option)}
                        onChange={() =>
                          handleCheckboxChange(setClimate, option)
                        }
                        color="primary"
                      />
                    }
                    label={option}
                  />
                ))}
              </Box>
            </FormControl>
          </Grid>

          {/* Temperature */}
          <Grid item xs={12}>
            <FormControl component="fieldset">
              <FormLabel component="legend" required sx={{ mb: 1 }}>
                متوسط درجة الحرارة
              </FormLabel>
              <Box sx={{ display: "flex", flexWrap: "wrap", gap: 2 }}>
                {["منخفضة", "متوسطة", "مرتفعة"].map((option) => (
                  <FormControlLabel
                    key={option}
                    control={
                      <Checkbox
                        checked={temperature.includes(option)}
                        onChange={() =>
                          handleCheckboxChange(setTemperature, option)
                        }
                        color="primary"
                      />
                    }
                    label={option}
                  />
                ))}
              </Box>
            </FormControl>
          </Grid>

          {/* Soil Type */}
          <Grid item xs={12}>
            <FormControl component="fieldset">
              <FormLabel component="legend" required sx={{ mb: 1 }}>
                نوع التربة
              </FormLabel>
              <Box sx={{ display: "flex", flexWrap: "wrap", gap: 2 }}>
                {["رملية", "طينية", "صخرية", "عضوية", "غير معروفة"].map(
                  (option) => (
                    <FormControlLabel
                      key={option}
                      control={
                        <Checkbox
                          checked={soilType.includes(option)}
                          onChange={() =>
                            handleCheckboxChange(setSoilType, option)
                          }
                          color="primary"
                        />
                      }
                      label={option}
                    />
                  )
                )}
              </Box>
            </FormControl>
          </Grid>

          {/* Pesticide */}
          <Grid item xs={12}>
            <FormControl component="fieldset">
              <FormLabel component="legend" sx={{ mb: 1 }}>
                ما الأدوية المستخدمة (اختياري)
              </FormLabel>
              <RadioGroup
                row
                value={pesticide}
                onChange={(e) => setPesticide(e.target.value)}
              >
                {["لا شيء", "سماد", "مبيد حشري"].map((option) => (
                  <FormControlLabel
                    key={option}
                    value={option}
                    control={<Radio color="primary" />}
                    label={option}
                  />
                ))}
              </RadioGroup>
            </FormControl>
          </Grid>

          {/* Submit Button */}
          <Grid item xs={12}>
            <Box sx={{ display: "flex", mt: 2 }}>
              <Button
                type="submit"
                variant="contained"
                color="primary"
                size="large"
                sx={{
                  px: 4,
                  py: 1,
                  borderRadius: 4,
                  "&:hover": {
                    transform: "translateY(-2px)",
                    transition: "all 0.3s",
                  },
                }}
              >
                إرسال
              </Button>
            </Box>
          </Grid>
        </Grid>
      </form>
    </FormPaper>
  );
};

export default PlantingForm;
