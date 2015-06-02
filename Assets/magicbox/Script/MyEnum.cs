using UnityEngine;
using System.Collections;

public enum OperateStep
{
	L = 0,
	L2,
	L0,
	R,
	R2,
	R0,
	U,
	U2,
	U0,
	D,
	D2,
	D0,
	F,
	F2,
	F0,
	B,
	B2,
	B0,
	X,
	X2,
	X0,
	Y,
	Y2,
	Y0,
	Z,
	Z2,
	Z0
}

public enum OperateSuit
{
	Left,
	Right,
	Up,
	Down,
	Front,
	Back,
	Entriety,
	MiddleX,
	MiddleY,
	MiddleZ
}

public enum OperatePiece
{
	Left,
	Top,
	Right
}

public enum State
{
	Operate,
	EditColor,
	EditFormula,
	OperateAndFormula
}

public enum MagicColor
{
	White = 0,
	Red,
	Blue,
	Yellow,
	Orange,
	Green,
	None
}

public enum CubeFaceStyle
{
	Up = 0,
	Front,
	Right,
	Down,
	Back,
	Left,
	None
}

public enum SingleCubeLocation
{
	UFR,
	FRD,
	RDB,
	DBL,
	BLU,
	LUF,
	UBR,
	FLD,
	UF,
	UR,
	UB,
	UL,
	FR,
	FD,
	FL,
	RD,
	RB,
	DB,
	DL,
	BL,
	U,
	F,
	R,
	D,
	B,
	L,
	None
}

public enum CubeMark
{
	WRB,
	WRG,
	WBO,
	WOG,
	YRB,
	YRG,
	YBO,
	YOG,
	WR,
	WB,
	WO,
	WG,
	YB,
	YG,
	YR,
	YO,
	BR,
	BO,
	GR,
	GO,
	W,
	R,
	B,
	Y,
	O,
	G,
	None
}

public enum CubeStyle
{
	Corner,
	Edge,
	Face,
	None
}