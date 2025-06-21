using System.ComponentModel.DataAnnotations;

namespace VehicleManagementAPI.Validation
{
    public class SaleDateAfterPurchaseAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not ValidationContext context)
                return true;
                
            var purchaseDate = context.ObjectType.GetProperty("PurchaseDate")?.GetValue(context.ObjectInstance) as DateTime?;
            var saleDate = context.ObjectType.GetProperty("SaleDate")?.GetValue(context.ObjectInstance) as DateTime?;
            
            if (saleDate.HasValue && purchaseDate.HasValue)
            {
                return saleDate.Value > purchaseDate.Value;
            }
            
            return true;
        }
        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var purchaseDate = validationContext.ObjectType.GetProperty("PurchaseDate")?.GetValue(validationContext.ObjectInstance) as DateTime?;
            var saleDate = validationContext.ObjectType.GetProperty("SaleDate")?.GetValue(validationContext.ObjectInstance) as DateTime?;
            
            if (saleDate.HasValue && purchaseDate.HasValue && saleDate.Value <= purchaseDate.Value)
            {
                return new ValidationResult("Sale date must be after purchase date.");
            }
            
            return ValidationResult.Success;
        }
    }
    
    public class PurchaseDateNotFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date <= DateTime.Now;
            }
            return true;
        }
        
        public override string FormatErrorMessage(string name)
        {
            return "Purchase date cannot be in the future.";
        }
    }
}