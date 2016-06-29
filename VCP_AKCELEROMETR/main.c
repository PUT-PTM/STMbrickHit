
#define HSE_VALUE ((uint32_t)8000000) /* STM32 discovery uses a 8Mhz external crystal */

#include "stm32f4xx_conf.h"
#include "stm32f4xx.h"
#include "stm32f4xx_gpio.h"
#include "stm32f4xx_rcc.h"
#include "stm32f4xx_exti.h"
#include "stm32f4xx_tim.h"
#include "usbd_cdc_core.h"
#include "usbd_usr.h"
#include "usbd_desc.h"
#include "usbd_cdc_vcp.h"
#include "usb_dcd_int.h"
#include "defines.h"
#include "tm_stm32f4_delay.h"
#include "tm_stm32f4_lis302dl_lis3dsh.h"

/*
 * The USB data must be 4 byte aligned if DMA is enabled. This macro handles
 * the alignment, if necessary (it's actually magic, but don't tell anyone).
 */
__ALIGN_BEGIN USB_OTG_CORE_HANDLE  USB_OTG_dev __ALIGN_END;


void init();
void ColorfulRingOfDeath(void);

/*
 * Define prototypes for interrupt handlers here. The conditional "extern"
 * ensures the weak declarations from startup_stm32f4xx.c are overridden.
 */
#ifdef __cplusplus
 extern "C" {
#endif

void SysTick_Handler(void);
void NMI_Handler(void);
void HardFault_Handler(void);
void MemManage_Handler(void);
void BusFault_Handler(void);
void UsageFault_Handler(void);
void SVC_Handler(void);
void DebugMon_Handler(void);
void PendSV_Handler(void);
void OTG_FS_IRQHandler(void);
void OTG_FS_WKUP_IRQHandler(void);

#ifdef __cplusplus
}
#endif



int main(void)
{
	TM_LIS302DL_LIS3DSH_t Axes_Data;
	/* Set up the system clocks */
	SystemInit();

	/* Initialize USB, IO, SysTick, and all those other things you do in the morning */
	init();

	/* Init delay */
	TM_DELAY_Init();
	timer();

	/* Init timer */
	timer5();

		/* Detect proper device */
	if (TM_LIS302DL_LIS3DSH_Detect() == TM_LIS302DL_LIS3DSH_Device_LIS302DL)
	{
		/* Initialize LIS302DL */
		TM_LIS302DL_LIS3DSH_Init(TM_LIS302DL_Sensitivity_2_3G, TM_LIS302DL_Filter_2Hz);
	}
	else if (TM_LIS302DL_LIS3DSH_Detect() == TM_LIS302DL_LIS3DSH_Device_LIS3DSH)
	{
		/* Initialize LIS3DSH */
		TM_LIS302DL_LIS3DSH_Init(TM_LIS3DSH_Sensitivity_2G, TM_LIS3DSH_Filter_800Hz);
	}
	else
	{
		/* Device is not recognized */

		/* Infinite loop */
		while (1);
	}

	/* Delay for 2 seconds */
	Delayms(2000);

	while (1)
	{
		/* Read axes data from initialized accelerometer */
		TM_LIS302DL_LIS3DSH_ReadAxes(&Axes_Data);

		TIM_Cmd(TIM2, ENABLE);
		if(TIM_GetFlagStatus(TIM2, TIM_FLAG_Update))
		{
			/* Turn LEDS on or off */
			/* Check X axes */
			if (Axes_Data.X > 200)
			{
				VCP_put_char('D');
				GPIOD->BSRRL = GPIO_Pin_14; // RED LED
			}
			else
			{
				GPIOD->BSRRH = GPIO_Pin_14;
			}
			if (Axes_Data.X < -200)
			{
				VCP_put_char('A');
				GPIOD->BSRRL = GPIO_Pin_12; // GREEN LED
			}
			else
			{
				GPIOD->BSRRH = GPIO_Pin_12;
			}

			uint8_t theByte;
			if (VCP_get_char(&theByte))
			{
				if(theByte == 'H')
				{
					GPIOD->BSRRL = GPIO_Pin_13; //ORANGE LED
					GPIOD->BSRRL = GPIO_Pin_15; // BLUE LED
					TM_Time2 = 10;
				}

				else if(theByte == 'B')
				{
					GPIOD->BSRRL = GPIO_Pin_13; // ORANGE LED
					TM_Time2 = 10;
				}

				else if(theByte == 'I')
				{
					GPIO_SetBits(GPIOD,GPIO_Pin_12);
					GPIO_SetBits(GPIOD,GPIO_Pin_13);
					GPIO_SetBits(GPIOD,GPIO_Pin_14);
					GPIO_SetBits(GPIOD,GPIO_Pin_15);

					TIM_Cmd(TIM5, ENABLE);
					TIM_ClearFlag(TIM5, TIM_FLAG_Update);

					uint8_t LED_Selector = 1;

					for (LED_Selector = 1; LED_Selector < 5; LED_Selector++)
					{
						TIM_Cmd(TIM5, ENABLE);
						while (!TIM_GetFlagStatus(TIM5, TIM_FLAG_Update));

						if (TIM_GetFlagStatus(TIM5, TIM_FLAG_Update))
						{

							switch (LED_Selector)
							{
							case 1:
								GPIO_ToggleBits(GPIOD, GPIO_Pin_12);
								break;
							case 2:
								GPIO_ToggleBits(GPIOD, GPIO_Pin_13);
								break;
							case 3:
								GPIO_ToggleBits(GPIOD, GPIO_Pin_14);
								break;
							case 4:
								GPIO_ToggleBits(GPIOD, GPIO_Pin_15);
								break;

							}
							TIM_ClearFlag(TIM5, TIM_FLAG_Update);
							TIM_Cmd(TIM5, DISABLE);
						}
					}
				}

				else if(theByte == 'R')
				{
					GPIOD->BSRRL = GPIO_Pin_15; // BLUE LED
					TM_Time2 = 10;
				}
			}
			if (0 == TM_Time2)
			{
				GPIOD->BSRRH = GPIO_Pin_13;
				GPIOD->BSRRH = GPIO_Pin_15;
			}

			TIM_ClearFlag(TIM2, TIM_FLAG_Update);
			TIM_Cmd(TIM2, DISABLE);
		}
	}
	return 0;
}


void init()
{
	/* STM32F4 discovery LEDs */
	GPIO_InitTypeDef LED_Config;

	/* Always remember to turn on the peripheral clock...  If not, you may be up till 3am debugging... */
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOD, ENABLE);
	LED_Config.GPIO_Pin = GPIO_Pin_12 | GPIO_Pin_13| GPIO_Pin_14| GPIO_Pin_15;
	LED_Config.GPIO_Mode = GPIO_Mode_OUT;
	LED_Config.GPIO_OType = GPIO_OType_PP;
	LED_Config.GPIO_Speed = GPIO_Speed_25MHz;
	LED_Config.GPIO_PuPd = GPIO_PuPd_NOPULL;
	GPIO_Init(GPIOD, &LED_Config);

	/* Setup SysTick or CROD! */
	if (SysTick_Config(SystemCoreClock / 1000))
	{
		ColorfulRingOfDeath();
	}

	/* Setup USB */
	USBD_Init(&USB_OTG_dev,
	            USB_OTG_FS_CORE_ID,
	            &USR_desc,
	            &USBD_CDC_cb,
	            &USR_cb);

	return;
}

void timer()
{
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM2, ENABLE);
    TIM_TimeBaseInitTypeDef TIM_TimeBaseStructure;
    TIM_TimeBaseStructure.TIM_Period = 150-1;
    TIM_TimeBaseStructure.TIM_Prescaler = 168000-1;
    TIM_TimeBaseStructure.TIM_ClockDivision = TIM_CKD_DIV1;
    TIM_TimeBaseStructure.TIM_CounterMode =  TIM_CounterMode_Up;
    TIM_TimeBaseInit(TIM2, &TIM_TimeBaseStructure);
}

void timer5()
{
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM5, ENABLE);
	TIM_TimeBaseInitTypeDef TIM_TimeBaseStructure;
	TIM_TimeBaseStructure.TIM_Period = 150-1;
	TIM_TimeBaseStructure.TIM_Prescaler = 168000-1;
	TIM_TimeBaseStructure.TIM_ClockDivision = TIM_CKD_DIV1;
	TIM_TimeBaseStructure.TIM_CounterMode =  TIM_CounterMode_Up;
	TIM_TimeBaseInit(TIM5, &TIM_TimeBaseStructure);
}

/*
 * Call this to indicate a failure.  Blinks the STM32F4 discovery LEDs
 * in sequence.  At 168Mhz, the blinking will be very fast - about 5 Hz.
 * Keep that in mind when debugging, knowing the clock speed might help
 * with debugging.
 */
void ColorfulRingOfDeath(void)
{
	uint16_t ring = 1;
	while (1)
	{
		uint32_t count = 0;
		while (count++ < 500000);

		GPIOD->BSRRH = (ring << 12);
		ring = ring << 1;
		if (ring >= 1<<4)
		{
			ring = 1;
		}
		GPIOD->BSRRL = (ring << 12);
	}
}

void NMI_Handler(void)       {}
void HardFault_Handler(void) { ColorfulRingOfDeath(); }
void MemManage_Handler(void) { ColorfulRingOfDeath(); }
void BusFault_Handler(void)  { ColorfulRingOfDeath(); }
void UsageFault_Handler(void){ ColorfulRingOfDeath(); }
void SVC_Handler(void)       {}
void DebugMon_Handler(void)  {}
void PendSV_Handler(void)    {}

void OTG_FS_IRQHandler(void)
{
  USBD_OTG_ISR_Handler (&USB_OTG_dev);
}

void OTG_FS_WKUP_IRQHandler(void)
{
  if(USB_OTG_dev.cfg.low_power)
  {
    *(uint32_t *)(0xE000ED10) &= 0xFFFFFFF9 ;
    SystemInit();
    USB_OTG_UngateClock(&USB_OTG_dev);
  }
  EXTI_ClearITPendingBit(EXTI_Line18);
}
