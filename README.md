
## MeadowAppsTryOut 
is a Visual Studio solution with a few small C# applications for the [Meadow F7 Development Board ](https://www.wildernesslabs.co/Meadow)

## Purpose 
The TryOuts are a collection of small Apps which are tested and working on the Meadow F7.
Main purpose is to have small examples for a certain connected device and learn the Meadow APIs so these examples can serve as a quickstart.


## Examples

| Project/Folder        | Purpose           | Extra Info        |
|:---------------:| --------------------- | -------------- |
| MeadowApp | The default starter template, but changed so it works for everyone. | Since Led.StartPulse gives problems the Led.StartBlink is used.  |
|  | |  |
| OledApp | Display text on 128x32 OLED (SSD1306 family). Also adds a Button to enable a Led  | D01=Led, D03=Button, D08=OLED-CLK , D07=OLED-SDA  [0.91 OLED 128x32 I2C SSD1306 ](https://www.aliexpress.com/item/32672229793.html?spm=a2g0s.9042311.0.0.27424c4deGstP2) |
|  | |  |



