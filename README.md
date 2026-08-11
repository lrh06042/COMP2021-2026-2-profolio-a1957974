# COMP2021 Portfolio

COMP2021 workshop and practical portfolio.

## Week 1

### Part A

- IntroTopic
- PayrollCalculator

### Part B

- PracB
- PracB.Tests

#### Refactoring Note

I extracted each command into a separate method so that each method has one clear responsibility. This makes the code easier to read, maintain, and test.

## Week 2

### Part A

- PracA
- PracA.Tests

#### Reflection

Moving from procedural code to a class-based design made the payroll program more organised. The data and related methods are now kept together in the Payroll class. This makes the code easier to reuse, test, and update.

Static typing helps me find mistakes while writing code. For example, hours use double, while rate and tax values use decimal. Sometimes I need an extra type conversion, but it makes the program safer and easier to understand.