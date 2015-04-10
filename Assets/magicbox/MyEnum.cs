using UnityEngine;
using System.Collections;

public enum OperateStep
{
	L,
	L0,
	L2,
	R,
	R0,
	R2,
	U,
	U0,
	U2,
	D,
	D0,
	D2,
	F,
	F0,
	F2,
	B,
	B0,
	B2,
	X,
	X0,
	X2,
	Y,
	Y0,
	Y2,
	Z,
	Z0,
	Z2
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

public enum CubeFace
{
	U = 0,
	F,
	R,
	D,
	B,
	L
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
	L
}

public enum CubeStyle
{
	Corner,
	Edge,
	Face,
	None
}