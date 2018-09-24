# Solution

## Counting bits

My first approach was *SimplePositiveBitCounter*. It is based on conversion to *BitArray* and simple counting. I knew that it can not be the right solution, but decided to implement it anyway for a reference and comparison.

Of course, it can not be the optimal solution, if we need count bits we should use bitwise operations.
My second solution is *BitwisePositiveBitCounter*. Each iteration it checks the last bit (*(input & 1) == 1*) and shifts input value right for one position (*input >>= 1*).

To be sure that *BitwisePositiveBitCounter* is faster I created *PositiveBitCounterBenchmarkTest*. When we do optimization we should profile always. And it showed that *BitwisePositiveBitCounter* is faster 6 times! I tried several trick to make it faster like using unchecked mode or using preallocated memory, but it did not add any significant improvements. Also I created additional test *Count_ValidInput_MaxInt* to ensure this edge case.

I decided to check what other guys did. For the future, to prevent cheating I suggest to use [bitbucket](https://bitbucket.org/), because it allows create free private repositories ;). Most interesting solutions are the solutions of [Daraciel](https://github.com/Daraciel/recruitment-challenges/blob/software-engineer-dotnet/Algorithms.CountingBits/PositiveBitCounter.cs) and of [mteknight](https://github.com/mteknight/payvision-recruitment-challenges/blob/software-engineer-dotnet/Algorithms.CountingBits/PositiveBitCounter.cs). But both failed on my *Count_ValidInput_MaxInt* ☺. Also, even if the first looks correct, it is significant slower than even my *SimplePositiveBitCounter*! So, always profile! I fixed the second solution and implemented *AlternativeBitwisePositiveBitCounter*. Even if it looks smart because of the way to count bits, it did not perform faster, it almost the same as my *BitwisePositiveBitCounter*.

And then I decided to get the maximum. I implemented the solution using c++ and PInvoke in *SuperBitwisePositiveBitCounter*. It gave me additional 5-10% of speed. And I think, if it did not have to deal with marshaling it would performs much faster.

## Refactoring fraud detection

The first thing I did, I converted *Refactoring.FraudDetection* project to **.NET Core**. I believe that .NET Core is the future of .NET and prefer stick with it.

Then I extracted Domain and Data layers (**Layered Architecture**). Data Layer is responsible for providing correct data, and Domain Layer is responsible for business logic, in our case it's detection of frauds. Domain Layer defines abstractions that Data Layer should follow (**DIP**). Thus Domain knows nothing about how data stored and stands in the center of our architecture, Data Layer takes in account the business logic (**Onion Architecture**).

All code were moved from *FraudRadar* and putted to separate classes according **SRP**. All dependencies in classes are defined through interfaces (**Constructor Injection**) for sake of flexibility and testability. I created unit tests for *FraudRadar* as a sample to demonstrate tooling I use: *AutoFixture*, *FluentAssertions*, *Moq*, *xUnit* (but not in this case for a simplicity and laziness). Also, I added simple logging with *Serilog* to facilitate maintenance.

*FraudRadar* have two main dependencies now: *IOrdersProvider* and *IFraudsDetector*. *FraudsDetector* uses different strategies (**Strategy Pattern**) to detect frauds in an implicit chain (**Chain-of-responsibility Pattern**). This approach helps easy create new detectors and define them in runtime, using configuration for example.

*IOrdersProvider* is implemented by two classes. *CsvOrdersProvider* fetches data from csv file, and do only this (**SRP**). In a future we can have other providers like *MsSqlOrdersProvider*. I used *CsvHelper* package because it's tested by 6 millions installations and I don't want waste time on implementation another one wheel.

*NormalizedOrdersProvider* uses other *IOrdersProvider* to get data and to apply data normalization (**Composition over Inheritance**, **Decorator Pattern**, **OCP**). Normalizers are implemented using **Visitor Pattern**. This pattern is natural to use when we have a sequential data processing like in this case. In a future we may have different types of objects where we want to apply similar logic. If something really unexpected is happened during the process I throw proper exception (**Fail fast**).

During refactoring I suspected a bugs in normalizers. So I implemented tests first to check it (*EmailNormalizerTests*, *StateNormalizerTests*, *StreetNormalizerTests*). I was pure **TDD** in action and I was right!

I did not use any **IoC/DI** container, did not create much unit tests to improve code coverage, did not create separate projects for layers (**YAGNI**). It a test assignment, after all ¯_(ツ)_/¯.

Thank you for your patience reading this! ☺