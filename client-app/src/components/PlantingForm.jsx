import React, { useState, useEffect } from "react";
import {
  FormControlLabel,
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
  CircularProgress,
} from "@mui/material";
import { styled } from "@mui/material/styles";
import api from "@/services/api";
import { useNavigate } from "react-router-dom";

const FormPaper = styled(Paper)(({ theme }) => ({
  padding: theme.spacing(4),
  borderRadius: 16,
  boxShadow: "0 8px 32px rgba(0, 0, 0, 0.1)",
}));

const PlantingForm = () => {
  const [formData, setFormData] = useState({
    locationType: "",
    areaDimensions: { length: "", width: "" },
    areaShape: "",
    climate: "",
    temperature: "",
    soilType: "",
    soilFertility: "",
    plantHealth: "",
    pesticide: "",
    selectedPlants: [],
  });
  const [errors, setErrors] = useState({});
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [availablePlants, setAvailablePlants] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchPlants = async () => {
      try {
        const { data } = await api.get("/api/Ghosn/AllPlants");
        const uniquePlants = [...new Set(data)].map((name, index) => ({
          id: index,
          plantName: name,
        }));
        setAvailablePlants(uniquePlants);
      } catch (error) {
        console.error("Error fetching plants:", error);
        setAvailablePlants([]);
      }
    };
    fetchPlants();
  }, []);

  const handleChange = (field) => (e) => {
    setFormData((prev) => ({
      ...prev,
      [field]: e.target.value,
    }));
    // Clear error when field is modified
    setErrors((prev) => ({ ...prev, [field]: "" }));
  };

  const handleAreaChange = (dimension) => (e) => {
    const value = e.target.value;
    setFormData((prev) => ({
      ...prev,
      areaDimensions: { ...prev.areaDimensions, [dimension]: value },
    }));
    // Clear error when field is modified
    setErrors((prev) => ({ ...prev, [`areaDimensions.${dimension}`]: "" }));
  };

  const validateForm = () => {
    const newErrors = {};

    // Required radio group fields
    const requiredFields = [
      "locationType",
      "areaShape",
      "climate",
      "temperature",
      "soilType",
      "soilFertility",
      "plantHealth",
      "pesticide",
    ];

    requiredFields.forEach((field) => {
      if (!formData[field]) {
        newErrors[field] = "هذا الحقل مطلوب";
      }
    });

    // Area dimensions validation
    if (!formData.areaDimensions.length) {
      newErrors["areaDimensions.length"] = "الطول مطلوب";
    } else if (parseFloat(formData.areaDimensions.length) <= 0) {
      newErrors["areaDimensions.length"] = "يجب أن يكون الطول أكبر من 0";
    }

    if (!formData.areaDimensions.width) {
      newErrors["areaDimensions.width"] = "العرض مطلوب";
    } else if (parseFloat(formData.areaDimensions.width) <= 0) {
      newErrors["areaDimensions.width"] = "يجب أن يكون العرض أكبر من 0";
    }

    return newErrors;
  };

  const mapToEnum = {
    locationType: {
      "حقل مفتوح": 0,
      "حديقة منزلية": 1,
      سقف: 2,
      أوعية: 3,
      "بيت زجاجي": 4,
    },
    areaShape: { مربع: 0, مستطيل: 1, "غير منتظم": 2 },
    climate: { "حار وجاف": 0, معتدل: 1, بارد: 2, رطب: 3, متغير: 4 },
    temperature: { منخفضة: 0, متوسطة: 1, مرتفعة: 2 },
    soilType: { رملية: 0, طينية: 1, صخرية: 2, عضوية: 3, "غير معروفة": 4 },
    soilFertility: { منخفضة: 0, متوسطة: 1, مرتفعة: 2 },
    plantHealth: { سليمة: 0, متوسطة: 1, "تحتاج إلى رعاية": 2 },
    pesticide: { "لا شيء": 0, سماد: 1, "مبيد حشري": 2 },
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const validationErrors = validateForm();
    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
      return;
    }

    setIsSubmitting(true);

    const { areaDimensions, selectedPlants, ...rest } = formData;
    const area =
      parseFloat(areaDimensions.length) * parseFloat(areaDimensions.width) || 0;

    const payload = {
      inputID: 0,
      areaSize: area,
      currentlyPlantedPlants: selectedPlants.map((plant) => ({
        plantName: plant.plantName,
      })),
      ...Object.fromEntries(
        Object.entries(rest).map(([key, value]) => [
          key,
          mapToEnum[key]?.[value] ?? null,
        ])
      ),
    };

    try {
      navigate("/planting-output", { state: { formData: payload } });
    } catch (error) {
      console.error("Error submitting data:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const radioGroups = [
    {
      label: "نوع مكان الزراعة",
      field: "locationType",
      options: ["حقل مفتوح", "حديقة منزلية", "سقف", "أوعية", "بيت زجاجي"],
      required: true,
    },
    {
      label: "شكل المنطقة",
      field: "areaShape",
      options: ["مربع", "مستطيل", "غير منتظم"],
      required: true,
    },
    {
      label: "المناخ",
      field: "climate",
      options: ["حار وجاف", "معتدل", "بارد", "رطب", "متغير"],
      required: true,
    },
    {
      label: "متوسط درجة الحرارة",
      field: "temperature",
      options: ["منخفضة", "متوسطة", "مرتفعة"],
      required: true,
    },
    {
      label: "نوع التربة",
      field: "soilType",
      options: ["رملية", "طينية", "صخرية", "عضوية", "غير معروفة"],
      required: true,
    },
    {
      label: "خصوبة التربة",
      field: "soilFertility",
      options: ["منخفضة", "متوسطة", "مرتفعة"],
      required: true,
    },
    {
      label: "حالة النباتات",
      field: "plantHealth",
      options: ["سليمة", "متوسطة", "تحتاج إلى رعاية"],
      required: true,
    },
    {
      label: "الأدوية المستخدمة",
      field: "pesticide",
      options: ["لا شيء", "سماد", "مبيد حشري"],
    },
  ];

  return (
    <FormPaper elevation={3} dir="rtl">
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
          {radioGroups.map(({ label, field, options, required }) => (
            <Grid item xs={12} key={field}>
              <FormControl
                fullWidth
                required={required}
                error={!!errors[field]}
              >
                <FormLabel>{label}</FormLabel>
                <RadioGroup
                  row
                  value={formData[field]}
                  onChange={handleChange(field)}
                >
                  {options.map((option) => (
                    <FormControlLabel
                      key={option}
                      value={option}
                      control={<Radio />}
                      label={option}
                    />
                  ))}
                </RadioGroup>
                {errors[field] && (
                  <Typography color="error" variant="caption">
                    {errors[field]}
                  </Typography>
                )}
              </FormControl>
            </Grid>
          ))}

          <Grid item xs={12}>
            <FormLabel required>الطول والعرض</FormLabel>
            <Grid container spacing={2}>
              <Grid item xs={6}>
                <TextField
                  label="طول المنطقة (متر)"
                  value={formData.areaDimensions.length}
                  onChange={handleAreaChange("length")}
                  fullWidth
                  type="number"
                  InputProps={{ inputProps: { min: 0 } }}
                  error={!!errors["areaDimensions.length"]}
                  helperText={errors["areaDimensions.length"]}
                />
              </Grid>
              <Grid item xs={6}>
                <TextField
                  label="عرض المنطقة (متر)"
                  value={formData.areaDimensions.width}
                  onChange={handleAreaChange("width")}
                  fullWidth
                  type="number"
                  InputProps={{ inputProps: { min: 0 } }}
                  error={!!errors["areaDimensions.width"]}
                  helperText={errors["areaDimensions.width"]}
                />
              </Grid>
            </Grid>
          </Grid>

          <Grid item xs={12}>
            <Autocomplete
              multiple
              options={availablePlants}
              getOptionLabel={(option) => option.plantName}
              value={formData.selectedPlants}
              onChange={(e, value) =>
                setFormData((prev) => ({ ...prev, selectedPlants: value }))
              }
              renderInput={(params) => (
                <TextField
                  {...params}
                  label="النباتات المزروعة حاليًا"
                  placeholder="اختر النباتات"
                />
              )}
            />
          </Grid>

          <Grid item xs={12}>
            <Button
              type="submit"
              variant="contained"
              color="primary"
              size="large"
              sx={{ px: 4, py: 1 }}
              disabled={isSubmitting}
            >
              {isSubmitting ? (
                <CircularProgress size={24} color="inherit" />
              ) : (
                "إرسال"
              )}
            </Button>
          </Grid>
        </Grid>
      </form>
    </FormPaper>
  );
};

export default PlantingForm;
