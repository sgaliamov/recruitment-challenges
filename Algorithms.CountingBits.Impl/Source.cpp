#include <list>

extern "C"
{
	struct ArrayStruct
	{
		int Data[32];
		int Length;
	};

	int GetBitCount(int input)
	{
		const int M1 = 0x55555555;
		const int M2 = 0x33333333;
		const int M4 = 0x0f0f0f0f;

		input -= (input >> 1) & M1;
		input = (input & M2) + ((input >> 2) & M2);
		input = (input + (input >> 4)) & M4;
		input += input >> 8;
		input += input >> 16;

		return input & 0x7f;
	}

	__declspec(dllexport) void Count(int input, ArrayStruct * a)
	{
		std::list<int> list;

		list.push_back(GetBitCount(input));

		for (int index = 0; input != 0; input >>= 1, index++)
		{
			if ((input & 1) == 1)
			{
				list.push_back(index);
			}
		}

		std::copy(list.begin(), list.end(), a->Data);
		a->Length = list.size();
	}
}
