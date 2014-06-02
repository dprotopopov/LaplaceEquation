
// MyCudafy.CudafyMulti
extern "C" __global__ void LaplaceSolver( double* prev, int prevLen0,  double* next, int nextLen0,  int* sizes, int sizesLen0,  int* extV, int extVLen0,  int* intV, int intVLen0,  double* w, int wLen0);
// MyCudafy.CudafyMulti
extern "C" __global__ void Copy( double* prev, int prevLen0,  double* next, int nextLen0);
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
extern "C" __global__ void LaplaceSolver( double* prev, int prevLen0,  double* next, int nextLen0,  int* sizes, int sizesLen0,  int* extV, int extVLen0,  int* intV, int intVLen0,  double* w, int wLen0)
{
	for (int i = blockDim.x * blockIdx.x + threadIdx.x; i < intV[(sizesLen0)]; i += blockDim.x * gridDim.x)
	{
		int num = 0;
		int j = 0;
		int num2 = i;
		while (j < sizesLen0)
		{
			num += (1 + num2 % (sizes[(j)] - 2)) * extV[(j)];
			num2 /= sizes[(j)] - 2;
			j++;
		}
		double num3 = prev[(num)] * w[(sizesLen0)];
		for (int k = 0; k < sizesLen0; k++)
		{
			num3 += (prev[(num - extV[(k)])] + prev[(num + extV[(k)])]) * w[(k)];
		}
		next[(num)] = num3;
	}
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
