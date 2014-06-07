
// MyCudafy.CudafyMulti
extern "C" __global__ void LaplaceSolver( double* prev, int prevLen0,  double* next, int nextLen0,  int* sizes, int sizesLen0,  int* extV, int extVLen0,  int* intV, int intVLen0,  double* w, int wLen0,  double* b, int bLen0,  double* c, int cLen0);
// MyCudafy.CudafyMulti
extern "C" __global__ void LaplaceSolverWithRelax( double* array, int arrayLen0,  int* sizes, int sizesLen0,  int* extV, int extVLen0,  int* intV, int intVLen0,  double* w, int wLen0,  double* b, int bLen0,  double* c, int cLen0, int p);
// MyCudafy.CudafyMulti
extern "C" __global__ void Copy( double* prev, int prevLen0,  double* next, int nextLen0);
// MyCudafy.CudafyMulti
extern "C" __global__ void Clear( double* array, int arrayLen0);
// MyCudafy.CudafyMulti
extern "C" __global__ void Square( double* prev, int prevLen0,  double* next, int nextLen0,  double* delta, int deltaLen0);
// MyCudafy.CudafyMulti
extern "C" __global__ void Delta( double* prev, int prevLen0,  double* next, int nextLen0,  double* delta, int deltaLen0);
// MyCudafy.CudafyMulti
extern "C" __global__ void Max( double* prev, int prevLen0,  double* next, int nextLen0);
// MyCudafy.CudafyMulti
extern "C" __global__ void Sum( double* prev, int prevLen0,  double* next, int nextLen0);

// MyCudafy.CudafyMulti
__constant__ double _a[100];
#define _aLen0 100
// MyCudafy.CudafyMulti
__constant__ double _b[1];
#define _bLen0 1
// MyCudafy.CudafyMulti
__constant__ double _c[1];
#define _cLen0 1
// MyCudafy.CudafyMulti
__constant__ int _sizes[2];
#define _sizesLen0 2
// MyCudafy.CudafyMulti
__constant__ double _lengths[2];
#define _lengthsLen0 2
// MyCudafy.CudafyMulti
__constant__ int _intV[3];
#define _intVLen0 3
// MyCudafy.CudafyMulti
__constant__ int _extV[3];
#define _extVLen0 3
// MyCudafy.CudafyMulti
__constant__ double _w[3];
#define _wLen0 3
// MyCudafy.CudafyMulti
extern "C" __global__ void LaplaceSolver( double* prev, int prevLen0,  double* next, int nextLen0,  int* sizes, int sizesLen0,  int* extV, int extVLen0,  int* intV, int intVLen0,  double* w, int wLen0,  double* b, int bLen0,  double* c, int cLen0)
{
	double num = 0.0;
	double num2 = 0.0;
	for (int i = blockDim.x * blockIdx.x + threadIdx.x; i < intV[(sizesLen0)]; i += blockDim.x * gridDim.x)
	{
		int num3 = 0;
		int j = 0;
		int num4 = i;
		while (j < sizesLen0)
		{
			int num5 = 1 + num4 % (sizes[(j)] - 2);
			num3 += num5 * extV[(j)];
			num4 /= sizes[(j)] - 2;
			j++;
		}
		double num6 = prev[(num3)];
		double num7 = num6 * w[(sizesLen0)];
		for (int k = 0; k < sizesLen0; k++)
		{
			num7 += (prev[(num3 - extV[(k)])] + prev[(num3 + extV[(k)])]) * w[(k)];
		}
		next[(num3)] = num7;
		double num8 = num6 - num7;
		double num9 = num6 + num7;
		num8 *= num8;
		num9 *= num9;
		num += num8;
		num2 += num9;
	}
	b[(blockDim.x * blockIdx.x + threadIdx.x)] = num;
	c[(blockDim.x * blockIdx.x + threadIdx.x)] = num2;
}
// MyCudafy.CudafyMulti
extern "C" __global__ void LaplaceSolverWithRelax( double* array, int arrayLen0,  int* sizes, int sizesLen0,  int* extV, int extVLen0,  int* intV, int intVLen0,  double* w, int wLen0,  double* b, int bLen0,  double* c, int cLen0, int p)
{
	double num = b[(blockDim.x * blockIdx.x + threadIdx.x)];
	double num2 = c[(blockDim.x * blockIdx.x + threadIdx.x)];
	for (int i = blockDim.x * blockIdx.x + threadIdx.x; i < intV[(sizesLen0)]; i += blockDim.x * gridDim.x)
	{
		int num3 = 0;
		int num4 = 0;
		int j = 0;
		int num5 = i;
		while (j < sizesLen0)
		{
			int num6 = 1 + num5 % (sizes[(j)] - 2);
			num4 += num6;
			num3 += num6 * extV[(j)];
			num5 /= sizes[(j)] - 2;
			j++;
		}
		if (num4 % 2 == p)
		{
			double num7 = array[(num3)];
			double num8 = num7 * w[(sizesLen0)];
			for (int k = 0; k < sizesLen0; k++)
			{
				num8 += (array[(num3 - extV[(k)])] + array[(num3 + extV[(k)])]) * w[(k)];
			}
			array[(num3)] = num8;
			double num9 = num7 - num8;
			double num10 = num7 + num8;
			num9 *= num9;
			num10 *= num10;
			num += num9;
			num2 += num10;
		}
	}
	b[(blockDim.x * blockIdx.x + threadIdx.x)] = num;
	c[(blockDim.x * blockIdx.x + threadIdx.x)] = num2;
}
// MyCudafy.CudafyMulti
extern "C" __global__ void Copy( double* prev, int prevLen0,  double* next, int nextLen0)
{
	for (int i = blockDim.x * blockIdx.x + threadIdx.x; i < prevLen0; i += blockDim.x * gridDim.x)
	{
		next[(i)] = prev[(i)];
	}
}
// MyCudafy.CudafyMulti
extern "C" __global__ void Clear( double* array, int arrayLen0)
{
	for (int i = blockDim.x * blockIdx.x + threadIdx.x; i < arrayLen0; i += blockDim.x * gridDim.x)
	{
		array[(i)] = 0.0;
	}
}
// MyCudafy.CudafyMulti
extern "C" __global__ void Square( double* prev, int prevLen0,  double* next, int nextLen0,  double* delta, int deltaLen0)
{
	for (int i = blockDim.x * blockIdx.x + threadIdx.x; i < prevLen0; i += blockDim.x * gridDim.x)
	{
		double num = next[(i)];
		num *= num;
		delta[(i)] = num;
	}
}
// MyCudafy.CudafyMulti
extern "C" __global__ void Delta( double* prev, int prevLen0,  double* next, int nextLen0,  double* delta, int deltaLen0)
{
	for (int i = blockDim.x * blockIdx.x + threadIdx.x; i < prevLen0; i += blockDim.x * gridDim.x)
	{
		double num = next[(i)] * (prev[(i)] - next[(i)]);
		num *= num;
		delta[(i)] = num;
	}
}
// MyCudafy.CudafyMulti
extern "C" __global__ void Max( double* prev, int prevLen0,  double* next, int nextLen0)
{
	for (int i = blockDim.x * blockIdx.x + threadIdx.x; i < nextLen0; i += blockDim.x * gridDim.x)
	{
		next[(i)] = 0.0;
		int num = 0;
		while (num * nextLen0 + i < prevLen0)
		{
			int num2 = num * nextLen0 + i;
			if (prev[(num2)] > next[(i)])
			{
				next[(i)] = prev[(num2)];
			}
			num++;
		}
	}
}
// MyCudafy.CudafyMulti
extern "C" __global__ void Sum( double* prev, int prevLen0,  double* next, int nextLen0)
{
	for (int i = blockDim.x * blockIdx.x + threadIdx.x; i < nextLen0; i += blockDim.x * gridDim.x)
	{
		next[(i)] = 0.0;
		int num = 0;
		while (num * nextLen0 + i < prevLen0)
		{
			int num2 = num * nextLen0 + i;
			next[(i)] += prev[(num2)];
			num++;
		}
	}
}
