using Application.Validation.Console;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace Test.Application.UnitTests
{
    [TestClass]
    public class ValidationServiceTest
    {
        [TestMethod]
        public void Should_validation_return_true_if_consistent_file()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.ConsistentFileLines);
            // ASSERT
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Should_validation_return_false_if_inconsistent_file_with_incorrect_conversion_request_line()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.InconsistentFileLinesWithIncorrectConversionRequestLine);
            // ASSERT
            result.Should().BeFalse();
        }

        [TestMethod]
        public void Should_validation_return_false_if_inconsistent_file_with_incorrect_exchange_rate_count_line()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.InconsistentFileLinesWithIncorrectExchangeRateCountLine);
            // ASSERT
            result.Should().BeFalse();
        }

        [TestMethod]
        public void Should_validation_return_false_if_inconsistent_file_with_at_least_one_incorrect_exchange_rate_line()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.InconsistentFileLinesWithAtLeastOneIncorrectExchangeRateLine);
            // ASSERT
            result.Should().BeFalse();
        }

        [TestMethod]
        public void Should_validation_return_false_if_inconsistent_file_with_less_than_3_lines()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.InconsistentFileLinesWithLessThan3Lines);
            // ASSERT
            result.Should().BeFalse();
        }

        [TestMethod]
        public void Should_validation_return_false_if_inconsistent_file_with_declared_count_exchange_rates_not_match_real_count()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.InconsistentFileLinesWithDeclaredCountAndRealCountNotMatch);
            // ASSERT
            result.Should().BeFalse();
        }

        [TestMethod]
        public void Should_validation_return_false_if_inconsistent_file_with_exchange_rate_containing_duplicate_currency()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.InconsistentFileLinesWitExchangeRateContainingDuplicateCurrency);
            // ASSERT
            result.Should().BeFalse();
        }

        [TestMethod]
        public void Should_validation_return_false_if_inconsistent_file_with_no_exchange_rate_containing_source_currency()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.InconsistentFileLinesWitNoExchangeRateContainingSourceCurrency);
            // ASSERT
            result.Should().BeFalse();
        }

        [TestMethod]
        public void Should_validation_return_false_if_inconsistent_file_with_no_exchange_rate_containing_target_currency()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.InconsistentFileLinesWitNoExchangeRateContainingTargetCurrency);
            // ASSERT
            result.Should().BeFalse();
        }
    }
}
