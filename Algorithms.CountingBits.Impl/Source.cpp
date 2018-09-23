extern "C"
{
	struct ArrayStruct
	{
		int Data[32];
	};

	__declspec(dllexport) void Count(int input, ArrayStruct * const output)
	{
		int counter = 1;
		for (int index = 0; input != 0; input >>= 1, index++)
		{
			if ((input & 1) == 1)
			{
				output->Data[counter++] = index;
			}
		}

		output->Data[0] = counter - 1;
	}
}
