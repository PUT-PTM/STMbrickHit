# STMbrickHit
## Overview
This is a project of simply game. It's known as Arkanoid or Brick Out. The game involves breaking bricks using a ball. This ball is 
bounced by paddle. Paddle can be control by keyboard (keys 'A' and 'D') and by using STM32F407.

## Description 
Code of game was wrote by using Visual Studio 2013 and this was connect with STM32F407 by using Virtual COM Port. The STM32 sends
data to PC. These data are receive by game and vice versa. Effects of this are shining LEDs: when we turn to the left the STM32,
the green LED is lighting, when we turn to the right the STM32, the red LED is lighting. In the time when the ball breaks the 
brick, the blue and orange LEDs are lighting.

## Tools 
- [CooCox CoIDE] (http://www.coocox.org/download/Tools/CoIDE-1.7.8.exe) 1.7.8 (C language)
- [Microsoft Visual Studio 2013 Professional] (https://www.visualstudio.com/products/visual-studio-professional-with-msdn-vs/) (C# language)
- [driver VCP] (http://www.st.com/content/st_com/en/products/development-tools/software-development-tools/stm32-software-development-tools/stm32-utilities/stsw-stm32102.html) 

## How to compile 
Start the file: STM_VCP_AKCELEROMETR.coproj from directory VCP_AKCELEROMETR and the file: Arkanoid.sln from directory Arkanoid. 
Upload a program from CooCox on STM32F407 and compile the program in VS2013.

## Future improvements
Improve the paddle's control by using STM32F407.

## Attributions 
- [Virtual COM Port] (https://github.com/xenovacivus/STM32DiscoveryVCP)
- [Accelerometer] (http://stm32f4-discovery.net/2014/09/library-35-lis302dl-or-lis3dsh-accelerometer/)

## License 
MIT license

## Credits 
Contributor: Oliwia Bartosik

The project was conducted during the Microprocessor Lab course held by the Institute of Control and Information Engineering, 
Poznan University of Technology.

Supervisor: Tomasz Mańkowski
