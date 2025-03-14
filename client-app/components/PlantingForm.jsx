import React, { useState } from 'react';
import { FormControlLabel, Checkbox, TextField, Button, Typography, Grid, Paper } from '@mui/material';
import axios from 'axios';

const PlantingForm = () => {
  const [locationTypes, setLocationTypes] = useState([]);
  const [areaDimensions, setAreaDimensions] = useState({ length: '', width: '' });
  const [climate, setClimate] = useState([]);
  const [temperature, setTemperature] = useState([]);
  const [soilType, setSoilType] = useState([]);
  const [soilFertility, setSoilFertility] = useState('');
  const [currentPlants, setCurrentPlants] = useState({ hasPlants: null, types: [] });
  const [plantHealth, setPlantHealth] = useState('');
  const [pesticide, setPesticide] = useState('');

  const handleCheckboxChange = (setState, value) => {
    setState((prev) => (prev.includes(value) ? prev.filter((item) => item !== value) : [...prev, value]));
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

    // إرسال البيانات إلى قاعدة البيانات عبر API
    axios.post('https://your-api-endpoint.com/submit', formData)
      .then(response => {
        console.log('تم إرسال البيانات بنجاح:', response.data);
        // يمكنك إضافة ردود الفعل بناءً على استجابة الخادم
      })
      .catch(error => {
        console.error('حدث خطأ أثناء إرسال البيانات:', error);
        // يمكن إضافة معالجة الخطأ هنا
      });
  };

  return (
    <Paper elevation={3} style={{ padding: '20px', maxWidth: '600px', margin: '20px auto' }}>
      <Typography variant="h5" gutterBottom align="center">نموذج معلومات الزراعة</Typography>
      <form onSubmit={handleSubmit}>
        <Grid container spacing={2}>
          <Grid item xs={12}>
            <Typography variant="h6" gutterBottom>
              نوع مكان الزراعة <span style={{ color: 'red' }}>*</span>
            </Typography>
            {['حقل مفتوح', 'حديقة منزلية', 'سقف', 'أوعية', 'بيت زجاجي'].map((option) => (
              <FormControlLabel
                key={option}
                control={<Checkbox checked={locationTypes.includes(option)} onChange={() => handleCheckboxChange(setLocationTypes, option)} />}
                label={option}
              />
            ))}
          </Grid>

          <Grid item xs={12}>
            <Typography variant="h6" gutterBottom>
              الطول والعرض <span style={{ color: 'red' }}>*</span>
            </Typography>
            <TextField label="طول المنطقة (متر)" value={areaDimensions.length} onChange={(e) => setAreaDimensions({ ...areaDimensions, length: e.target.value })} fullWidth />
            <TextField label="عرض المنطقة (متر)" value={areaDimensions.width} onChange={(e) => setAreaDimensions({ ...areaDimensions, width: e.target.value })} fullWidth style={{ marginTop: '10px' }} />
          </Grid>

          <Grid item xs={12}>
            <Typography variant="h6" gutterBottom>
              المناخ <span style={{ color: 'red' }}>*</span>
            </Typography>
            {['حار وجاف', 'معتدل', 'بارد', 'رطب', 'متغير'].map((option) => (
              <FormControlLabel
                key={option}
                control={<Checkbox checked={climate.includes(option)} onChange={() => handleCheckboxChange(setClimate, option)} />}
                label={option}
              />
            ))}
          </Grid>

          <Grid item xs={12}>
            <Typography variant="h6" gutterBottom>
              متوسط درجة الحرارة <span style={{ color: 'red' }}>*</span>
            </Typography>
            {['منخفضة', 'متوسطة', 'مرتفعة'].map((option) => (
              <FormControlLabel
                key={option}
                control={<Checkbox checked={temperature.includes(option)} onChange={() => handleCheckboxChange(setTemperature, option)} />}
                label={option}
              />
            ))}
          </Grid>

          <Grid item xs={12}>
            <Typography variant="h6" gutterBottom>
              نوع التربة <span style={{ color: 'red' }}>*</span>
            </Typography>
            {['رملية', 'طينية', 'صخرية', 'عضوية', 'غير معروفة'].map((option) => (
              <FormControlLabel
                key={option}
                control={<Checkbox checked={soilType.includes(option)} onChange={() => handleCheckboxChange(setSoilType, option)} />}
                label={option}
              />
            ))}
          </Grid>

          <Grid item xs={12}>
            <Typography variant="h6" gutterBottom>
              ما الأدوية المستخدمة (اختياري)
            </Typography>
            {['لا شيء', 'سماد', 'مبيد حشري'].map((option) => (
              <FormControlLabel
                key={option}
                control={<Checkbox checked={pesticide === option} onChange={() => setPesticide(option)} />}
                label={option}
              />
            ))}
          </Grid>

          <Grid item xs={12} style={{ textAlign: 'center' }}>
            <Button type="submit" variant="contained" color="primary">إرسال</Button>
          </Grid>
        </Grid>
      </form>
    </Paper>
  );
};

export default PlantingForm;
