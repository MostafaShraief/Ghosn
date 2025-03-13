import React, { useState } from 'react';
import { FormControl, InputLabel, MenuItem, Select, TextField } from '@mui/material';

const PlantingForm = () => {
  const [location, setLocation] = useState('');
  const [area, setArea] = useState('');
  const [areaShape, setAreaShape] = useState('');
  const [climate, setClimate] = useState('');
  const [temperature, setTemperature] = useState('');
  const [soilType, setSoilType] = useState('');
  const [soilFertility, setSoilFertility] = useState('');
  const [currentPlants, setCurrentPlants] = useState([]);
  const [plantHealth, setPlantHealth] = useState('');
  const [pesticide, setPesticide] = useState('');

  const handlePlantChange = (event) => {
    const value = event.target.value;
    setCurrentPlants(typeof value === 'string' ? value.split(',') : value);
  };

  return (
    <div>
      <FormControl fullWidth style={{ marginBottom: '16px', marginTop: '16px' }}>
        <InputLabel>نوع مكان الزراعة</InputLabel>
        <Select sx={{ minWidth: 220 }} value={location} onChange={(e) => setLocation(e.target.value)}>
          <MenuItem value="openField">أرض زراعية مفتوحة</MenuItem>
          <MenuItem value="homeGarden">حديقة منزلية</MenuItem>
          <MenuItem value="roof">سطح المنزل</MenuItem>
          <MenuItem value="containers">أصص أو حاويات زراعية</MenuItem>
          <MenuItem value="greenhouse">بيوت محمية</MenuItem>
        </Select>
      </FormControl>

      <TextField
        label="ما هي مساحة المنطقة؟ (بالأمتار أو القدم)"
        value={area}
        onChange={(e) => setArea(e.target.value)}
        fullWidth
        style={{ marginBottom: '16px' }}
      />

      <FormControl fullWidth style={{ marginBottom: '16px' }}>
        <InputLabel>شكل المنطقة</InputLabel>
        <Select sx={{ minWidth: 220 }} value={areaShape} onChange={(e) => setAreaShape(e.target.value)}>
          <MenuItem value="square">مربعة</MenuItem>
          <MenuItem value="rectangle">مستطيلة</MenuItem>
          <MenuItem value="irregular">غير منتظمة</MenuItem>
        </Select>
      </FormControl>

      <FormControl fullWidth style={{ marginBottom: '16px' }}>
        <InputLabel>المناخ</InputLabel>
        <Select sx={{ minWidth: 220 }} value={climate} onChange={(e) => setClimate(e.target.value)}>
          <MenuItem value="hotDry">حار وجاف</MenuItem>
          <MenuItem value="moderate">معتدل</MenuItem>
          <MenuItem value="cold">بارد</MenuItem>
          <MenuItem value="humid">رطب</MenuItem>
          <MenuItem value="variable">متقلب</MenuItem>
        </Select>
      </FormControl>

      <TextField
        style={{ marginBottom: '16px' }}
        label="متوسط درجة الحرارة أو اختر لا أعرف"
        value={temperature}
        onChange={(e) => setTemperature(e.target.value)}
        fullWidth
      />

      <FormControl fullWidth style={{ marginBottom: '16px' }}>
        <InputLabel>نوع التربة</InputLabel>
        <Select sx={{ minWidth: 220 }} value={soilType} onChange={(e) => setSoilType(e.target.value)}>
          <MenuItem value="sandy">تربة رملية</MenuItem>
          <MenuItem value="clay">تربة طينية</MenuItem>
          <MenuItem value="rocky">تربة صخرية</MenuItem>
          <MenuItem value="organic">تربة عضوية</MenuItem>
          <MenuItem value="unknown">لا أعرف</MenuItem>
        </Select>
      </FormControl>

      <FormControl fullWidth style={{ marginBottom: '16px' }}>
        <InputLabel>مستوى خصوبة التربة</InputLabel>
        <Select sx={{ minWidth: 220 }} value={soilFertility} onChange={(e) => setSoilFertility(e.target.value)}>
          <MenuItem value="low">منخفضة</MenuItem>
          <MenuItem value="medium">متوسطة</MenuItem>
          <MenuItem value="high">عالية</MenuItem>
        </Select>
      </FormControl>

      <FormControl fullWidth style={{ marginBottom: '16px' }}>
        <InputLabel>النباتات المزروعة حاليًا</InputLabel>
        <Select sx={{ minWidth: 220 }} multiple value={currentPlants} onChange={handlePlantChange}>
          <MenuItem value="none">لا يوجد</MenuItem>
          <MenuItem value="custom">إدخال يدوي</MenuItem>
        </Select>
      </FormControl>

      <FormControl fullWidth style={{ marginBottom: '16px' }}>
        <InputLabel>حالة النباتات الحالية</InputLabel>
        <Select sx={{ minWidth: 220 }} value={plantHealth} onChange={(e) => setPlantHealth(e.target.value)}>
          <MenuItem value="healthy">صحية</MenuItem>
          <MenuItem value="medium">متوسطة</MenuItem>
          <MenuItem value="needsCare">تحتاج عناية</MenuItem>
        </Select>
      </FormControl>

      <FormControl fullWidth style={{ marginBottom: '16px' }}>
        <InputLabel>ما الأدوية المستخدمة؟ (اختياري)</InputLabel>
        <Select sx={{ minWidth: 220 }} value={pesticide} onChange={(e) => setPesticide(e.target.value)}>
          <MenuItem value="none">لا يوجد</MenuItem>
          <MenuItem value="fertilizer">سماد</MenuItem>
          <MenuItem value="insecticide">مبيد حشرات</MenuItem>
        </Select>
      </FormControl>
    </div>
  );
};

export default PlantingForm;
