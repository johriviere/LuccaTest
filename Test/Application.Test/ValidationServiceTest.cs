using Application.Validation.Console;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Application.Test
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
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Should_validation_return_false_if_inconsistent_file_with_incorrect_conversion_request_line()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.InconsistentFileLinesWithIncorrectConversionRequestLine);
            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Should_validation_return_false_if_inconsistent_file_with_incorrect_exchange_rate_count_line()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.InconsistentFileLinesWithIncorrectExchangeRateCountLine);
            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Should_validation_return_false_if_inconsistent_file_with_at_least_one_incorrect_exchange_rate_line()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.InconsistentFileLinesWithAtLeastOneIncorrectExchangeRateLine);
            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Should_validation_return_false_if_inconsistent_file_with_less_than_3_lines()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.InconsistentFileLinesWithLessThan3Lines);
            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Should_validation_return_false_if_inconsistent_file_with_declared_count_exchange_rates_not_match_real_count()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.InconsistentFileLinesWithDeclaredCountAndRealCountNotMatch);
            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Should_validation_return_false_if_inconsistent_file_with_exchange_rate_containing_duplicate_currency()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.InconsistentFileLinesWitExchangeRateContainingDuplicateCurrency);
            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Should_validation_return_false_if_inconsistent_file_with_no_exchange_rate_containing_source_currency()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.InconsistentFileLinesWitNoExchangeRateContainingSourceCurrency);
            // ASSERT
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Should_validation_return_false_if_inconsistent_file_with_no_exchange_rate_containing_target_currency()
        {
            // ARRANGE + ACT
            ValidationService svc = new ValidationService();
            var result = svc.IsValid(Fake.InconsistentFileLinesWitNoExchangeRateContainingTargetCurrency);
            // ASSERT
            Assert.IsFalse(result);
        }
    }
}
