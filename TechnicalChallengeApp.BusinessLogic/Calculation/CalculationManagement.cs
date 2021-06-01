using System;
using TechnicalChallengeApp.Model.Calculation;
using TechnicalChallengeApp.Model.Global;

namespace TechnicalChallengeApp.BusinessLogic.Calculation
{
    public class CalculationManagement : ICalculationManagement
    {
        /// <summary>
        /// Performs the calculation operation
        /// </summary>
        /// <param name="request">The request information, which includes the values and calculation type</param>
        /// <returns>The calculated value</returns>
        public ApiResponse<CalculationResponse> PerformCalculation(CalculationRequest request)
        {
            var left = request.LastTotal;
            var right = request.ValueToApply;

            var apiResponse = new ApiResponse<CalculationResponse>
            {
                Data = new CalculationResponse
                {
                    Total = 0 // Placeholder total value that will be overwritten and set by our logic below
                },

                IsSuccess = true,
                Message = "Calculation operation completed"
            };

            // Our variable for storing the total value, which we
            // will add into our API response
            var totalValue = 0f;

            switch (request.CalculationType)
            {
                case CalculationType.Add:
                    totalValue = left + right;
                    break;

                case CalculationType.Subtract:
                    totalValue = left - right;
                    break;

                case CalculationType.Multiply:
                    totalValue = left * right;
                    break;

                case CalculationType.Divide:
                    try
                    {
                        totalValue = left / right;
                    }
                    catch (DivideByZeroException)
                    {
                        apiResponse.IsSuccess = false;
                        apiResponse.Message = "You cannot divide by zero";
                    }
                    break;
            }

            return apiResponse;
        }
    }
}