
## MeadowAppsTryOut 
is a Visual Studio solution with a few small C# applications for the [Meadow F7 Development Board ](https://www.wildernesslabs.co/Meadow)

## Purpose 
The TryOuts are a collection of small Apps which are tested and working on the Meadow F7.
Main purpose is to have small examples for a certain connected device and learn the Meadow APIs so these examples can serve as a quick-start.


## Examples

| Project/Folder        | Purpose           | Extra Info        |
|:---------------:| --------------------- | -------------- |
| InputValueChanged | Digital input with an event handler. | There are a few ways to handle an input (like a pushbutton) and react on changes. .  |
|  | |  |
| MeadowApp | The default starter template (HelloLed), blinking the internal LED in 3 colors | It was reported that Led.StartPulse gives problems for some, so Led.StartBlink is used here. |
|  | |  |
| OledApp | Display text on 128x32 OLED (SSD1306 family) and a Button enables a Led  | D01=Led, D03=Button, D08=OLED-CLK , D07=OLED-SDA  [0.91 OLED 128x32 I2C SSD1306 ](https://www.aliexpress.com/item/32672229793.html?spm=a2g0s.9042311.0.0.27424c4deGstP2) |
|  | |  |
| Uln2003App | Motor stepper, using ULN2003 motor driver board. | D01=ULN2003-A, D02=ULN2003-B, D03=ULN2003-C, D04=ULN2003-D  [ULN2003 board](https://nl.aliexpress.com/item/32711426614.html?src=google&src=google&albch=shopping&acnt=494-037-6276&isdl=y&slnk=&plac=&mtctp=&albbt=Google_7_shopping&aff_platform=google&aff_short_key=UneMJZVf&&albagn=888888&albcp=6459980570&albag=76980386066&trgt=743612850714&crea=nl32711426614&netw=u&device=c&gclid=Cj0KCQiA-bjyBRCcARIsAFboWg2y2sQtoZg8n_mVO2yY5DvV6MwEBc1kMVcpUydNrslsbpW6-QmPCY4aAmaUEALw_wcB&gclsrc=aw.ds) |
|  | |  |








