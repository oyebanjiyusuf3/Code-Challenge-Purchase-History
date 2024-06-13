using CloudComputeResourceTracker.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CloudComputeResourceTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseController(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        [HttpGet("all-purchases")]
        public IActionResult GetAllPurchases()
        {
            //Console.WriteLine("Controller action reached!");
            var purchases = _purchaseRepository.GetPurchases()

                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    Date = p.PurchasedAt,
                    // p.UnitPrice,
                    TotalCost = p.Quantity * p.UnitPrice,
                    // p.Quantity
                });

            return Ok(purchases);

        }

        [HttpGet("single-purchase/{id}")]
        public IActionResult GetSinglePurchase(long id)
        {
            var purchase = _purchaseRepository.GetPurchase(id);

            if (purchase == null)
                return NotFound();

            var result = new
            {
                purchase.Name,
                Date = purchase.PurchasedAt,
                TotalCost = purchase.Quantity * purchase.UnitPrice,
                purchase.Quantity,
                purchase.UnitPrice,
                purchase.Description
            };

            return Ok(result);
        }

        [HttpGet("summary-statistics")]
        public IActionResult GetSummaryStatistics()
        {
            var purchases = _purchaseRepository.GetPurchases();

            // Time series of spend per month
            var spendPerMonth = purchases
                .GroupBy(p => new { p.PurchasedAt.Year, p.PurchasedAt.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalSpend = g.Sum(p => p.Quantity * p.UnitPrice)
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToList();

            // Most expensive month
            var mostExpensiveMonth = spendPerMonth
                .OrderByDescending(g => g.TotalSpend)
                .FirstOrDefault();

            // Month with the most units bought
            var monthWithMostUnits = purchases
                .GroupBy(p => new { p.PurchasedAt.Year, p.PurchasedAt.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalUnits = g.Sum(p => p.Quantity)
                })
                .OrderByDescending(g => g.TotalUnits)
                .FirstOrDefault();

            // Product name related to the most expensive purchase
            /*This return single product
            var mostExpensivePurchase = purchases
                .OrderByDescending(p => p.Quantity * p.UnitPrice)
                .FirstOrDefault(); */

            /*Product name related to the most expensive purchase
            I have the product name related to the most expensive purchase
            and the total quantity for all purchases with the same product name.
            Also, mostExpensivePurchase contains the product name and the highest total cost */
            var mostExpensivePurchase = purchases
                .GroupBy(p => p.Name)
                .Select(group => new
                {
                    Name = group.Key,
                    TotalCost = group.Sum(p => p.UnitPrice * p.Quantity)
                })
                .OrderByDescending(p => p.TotalCost)
                .FirstOrDefault();

            // Product name with the most units bought
            var productWithMostUnits = purchases
                .GroupBy(p => p.Name)
                .Select(g => new
                {
                    ProductName = g.Key,
                    TotalUnits = g.Sum(p => p.Quantity)
                })
                .OrderByDescending(g => g.TotalUnits)
                .FirstOrDefault();

            var summaryStatistics = new
            {
                SpendPerMonth = spendPerMonth,
                MostExpensiveMonth = mostExpensiveMonth,
                MonthWithMostUnits = monthWithMostUnits,
                //ProductNameMostExpensivePurchase = new[]{new{ Name = mostExpensivePurchase?.Name,Quantity = mostExpensivePurchase?.Quantity  }},          
                ProductNameMostExpensivePurchase = mostExpensivePurchase,
                //ProductNameMostUnitsBought = productWithMostUnits?.ProductName
                ProductNameMostUnitsBought = productWithMostUnits
            };

            return Ok(summaryStatistics);
        }


    }
}
