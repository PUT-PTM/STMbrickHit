#ifndef AKCELEROMETR_H
#include "stm32f4_discovery_lis302dl.h"

uint8_t acc_x = 0;
uint8_t acc_y = 0;
uint8_t acc_z = 0;

#define AKC_ReadX(void)	LIS302DL_Read(&acc_x, LIS302DL_OUT_X_ADDR, 1)
#define AKC_ReadY(void)	LIS302DL_Read(&acc_y, LIS302DL_OUT_Y_ADDR, 1)
#define AKC_ReadZ(void)	LIS302DL_Read(&acc_z, LIS302DL_OUT_Z_ADDR, 1)

void AKC_Init()
{
	LIS302DL_InitTypeDef AKC_InitStructure;
	AKC_InitStructure.Axes_Enable = LIS302DL_XYZ_ENABLE;
	AKC_InitStructure.Full_Scale = LIS302DL_FULLSCALE_2_3;
	AKC_InitStructure.Power_Mode = LIS302DL_LOWPOWERMODE_ACTIVE;
	AKC_InitStructure.Output_DataRate = LIS302DL_DATARATE_100;
	AKC_InitStructure.Self_Test = LIS302DL_SELFTEST_NORMAL;
	LIS302DL_Init(&AKC_InitStructure);
}

void AktualizujAKC()
{
	AKC_ReadX();
	AKC_ReadY();
	AKC_ReadZ();
}

#endif
