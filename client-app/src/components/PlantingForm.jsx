import React, { useState, useEffect } from "react";
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
  Autocomplete,
} from "@mui/material";
import { styled } from "@mui/material/styles";
import axios from "axios";
import { useNavigate } from "react-router-dom";

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
  const [areaShape, setAreaShape] = useState("");
  const [climate, setClimate] = useState([]);
  const [temperature, setTemperature] = useState([]);
  const [soilType, setSoilType] = useState([]);
  const [soilFertility, setSoilFertility] = useState("");
  const [plantHealth, setPlantHealth] = useState("");
  const [pesticide, setPesticide] = useState("");
  const [availablePlants, setAvailablePlants] = useState([]);
  const [selectedPlants, setSelectedPlants] = useState([]);

  const navigate = useNavigate();
  useEffect(() => {
    const fetchPlants = async () => {
      try {
        const response = await axios.get("/api/Ghosn/AllPlants");

        if (Array.isArray(response.data)) {
          const uniquePlantNames = new Set();
          const transformedPlants = [];

          response.data.forEach((plantName, index) => {
            if (!uniquePlantNames.has(plantName)) {
              uniquePlantNames.add(plantName);
              transformedPlants.push({
                id: index,
                plantName: plantName,
              });
            } else {
              console.warn(`Duplicate plant name found: ${plantName}`);
            }
          });

          setAvailablePlants(transformedPlants);
        } else {
          console.error("API response has unexpected format:", response.data);
          setAvailablePlants([]);
        }
      } catch (error) {
        console.error("Error fetching plants:", error);
      }
    };

    fetchPlants();
  }, []);

  const handleCheckboxChange = (setState, value) => {
    setState((prev) =>
      prev.includes(value)
        ? prev.filter((item) => item !== value)
        : [...prev, value]
    );
  };

  const handleSubmit = (event) => {
    event.preventDefault();

    // --- Mapping to Backend Enums ---
    const mapLocationType = (types) => {
      if (types.length === 0) return null;
      const type = types[0];
      switch (type) {
        case "حقل مفتوح":
          return 0;
        case "حديقة منزلية":
          return 1;
        case "سقف":
          return 2;
        case "أوعية":
          return 3;
        case "بيت زجاجي":
          return 4;
        default:
          return null;
      }
    };

    const mapAreaShape = (shape) => {
      switch (shape) {
        case "مربع":
          return 0;
        case "مستطيل":
          return 1;
        case "غير منتظم":
          return 2;
        default:
          return null;
      }
    };

    const mapClimateType = (climates) => {
      if (climates.length === 0) return null;
      const climateType = climates[0];
      switch (climateType) {
        case "حار وجاف":
          return 0;
        case "معتدل":
          return 1;
        case "بارد":
          return 2;
        case "رطب":
          return 3;
        case "متغير":
          return 4;
        default:
          return null;
      }
    };

    const mapTemperature = (temps) => {
      if (temps.length === 0) return null;
      const temp = temps[0];
      switch (temp) {
        case "منخفضة":
          return 0;
        case "متوسطة":
          return 1;
        case "مرتفعة":
          return 2;
        default:
          return null;
      }
    };

    const mapSoilType = (types) => {
      if (types.length === 0) return null;
      const type = types[0];
      switch (type) {
        case "رملية":
          return 0;
        case "طينية":
          return 1;
        case "صخرية":
          return 2;
        case "عضوية":
          return 3;
        case "غير معروفة":
          return 4;
        default:
          return null;
      }
    };

    const mapSoilFertility = (fertility) => {
      switch (fertility) {
        case "منخفضة":
          return 0;
        case "متوسطة":
          return 1;
        case "مرتفعة":
          return 2;
        default:
          return null;
      }
    };
    const mapPlantHealth = (health) => {
      switch (health) {
        case "سليمة":
          return 0;
        case "متوسطة":
          return 1;
        case "تحتاج إلى رعاية":
          return 2;
        default:
          return null;
      }
    };

    const mapPesticide = (pesticide) => {
      switch (pesticide) {
        case "لا شيء":
          return 0;
        case "سماد":
          return 1;
        case "مبيد حشري":
          return 2;
        default:
          return null;
      }
    };

    // --- Calculate Area ---
    const area =
      parseFloat(areaDimensions.length) * parseFloat(areaDimensions.width);

    // --- Prepare currentlyPlantedPlants ---
    const plantedPlantsPayload = selectedPlants.map((plant) => ({
      plantName: plant.plantName, // Make sure this matches your API's expected property
    }));

    // --- Constructing the Payload ---
    const formData = {
      inputID: 0,
      locationType: mapLocationType(locationTypes),
      areaSize: isNaN(area) ? 0 : area,
      areaShape: mapAreaShape(areaShape),
      climate: mapClimateType(climate),
      temperature: mapTemperature(temperature),
      soilType: mapSoilType(soilType),
      soilFertilityLevel: mapSoilFertility(soilFertility),
      plantsStatus: mapPlantHealth(plantHealth),
      medicationsUsed: mapPesticide(pesticide),
      currentlyPlantedPlants: plantedPlantsPayload,
    };

    axios
      .post("/api/Ghosn/GeneratePlan", formData)
      .then((response) => {
        console.log("Data submitted successfully:", response.data);
      })
      .catch((error) => {
        console.error("Error submitting data:", error);
      });

    navigate("/planting-output", { state: { formData } });
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
            <FormControl component="fieldset" fullWidth>
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

          {/* Area Shape */}
          <Grid item xs={12}>
            <FormControl component="fieldset" fullWidth required>
              <FormLabel component="legend">شكل المنطقة</FormLabel>
              <RadioGroup
                row
                value={areaShape}
                onChange={(e) => setAreaShape(e.target.value)}
                name="area-shape"
              >
                <FormControlLabel
                  value="مربع"
                  control={<Radio />}
                  label="مربع"
                />
                <FormControlLabel
                  value="مستطيل"
                  control={<Radio />}
                  label="مستطيل"
                />
                <FormControlLabel
                  value="غير منتظم"
                  control={<Radio />}
                  label="غير منتظم"
                />
              </RadioGroup>
            </FormControl>
          </Grid>

          {/* Climate */}
          <Grid item xs={12}>
            <FormControl component="fieldset" fullWidth>
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
            <FormControl component="fieldset" fullWidth>
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
            <FormControl component="fieldset" fullWidth>
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

          {/* Soil Fertility */}
          <Grid item xs={12}>
            <FormControl component="fieldset" fullWidth required>
              <FormLabel component="legend">خصوبة التربة</FormLabel>
              <RadioGroup
                row
                value={soilFertility}
                onChange={(e) => setSoilFertility(e.target.value)}
                name="soil-fertility"
              >
                <FormControlLabel
                  value="منخفضة"
                  control={<Radio />}
                  label="منخفضة"
                />
                <FormControlLabel
                  value="متوسطة"
                  control={<Radio />}
                  label="متوسطة"
                />
                <FormControlLabel
                  value="مرتفعة"
                  control={<Radio />}
                  label="مرتفعة"
                />
              </RadioGroup>
            </FormControl>
          </Grid>

          {/* Plant Health */}
          <Grid item xs={12}>
            <FormControl component="fieldset" fullWidth required>
              <FormLabel component="legend">حالة النباتات</FormLabel>
              <RadioGroup
                row
                value={plantHealth}
                onChange={(e) => setPlantHealth(e.target.value)}
                name="plant-health"
              >
                <FormControlLabel
                  value="سليمة"
                  control={<Radio />}
                  label="سليمة"
                />
                <FormControlLabel
                  value="متوسطة"
                  control={<Radio />}
                  label="متوسطة"
                />
                <FormControlLabel
                  value="تحتاج إلى رعاية"
                  control={<Radio />}
                  label="تحتاج إلى رعاية"
                />
              </RadioGroup>
            </FormControl>
          </Grid>

          {/* Pesticide */}
          <Grid item xs={12}>
            <FormControl component="fieldset" fullWidth>
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

          {/* Currently Planted Plants (Autocomplete) */}
          <Grid item xs={12}>
            <FormControl fullWidth>
              <FormLabel component="legend">النباتات المزروعة حاليًا</FormLabel>
              <Autocomplete
                multiple
                id="currently-planted-plants"
                options={availablePlants}
                getOptionLabel={(option) => option?.plantName ?? ""}
                value={selectedPlants}
                onChange={(event, newValue) => {
                  setSelectedPlants(newValue);
                }}
                filterSelectedOptions
                renderInput={(params) => (
                  <TextField
                    {...params}
                    variant="outlined"
                    label="اختر النباتات"
                    placeholder="ابحث عن نبات"
                  />
                )}
              />
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
