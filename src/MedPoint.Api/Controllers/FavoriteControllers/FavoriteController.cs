using MedPoint.Service.Dtos.CartItems;
using MedPoint.Service.Dtos.Favorites;
using MedPoint.Service.Interfaces.ICartItemServices;
using MedPoint.Service.Interfaces.IFavoriteServices;
using Microsoft.AspNetCore.Mvc;

namespace MedPoint.Api.Controllers.FavoriteControllers
{
    public class FavoriteController(IFavoriteService favoriteService) : BaseController
    {
        private readonly IFavoriteService favoriteService = favoriteService;

        [HttpPost]

        public async Task<IActionResult> PostAsync(FavoriteForCreationDto dto, CancellationToken cancellationToken = default)
        {
            return Ok(await favoriteService.AddAsync(dto, cancellationToken));
        }

        [HttpGet]

        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Ok(await favoriteService.GetAllAsync(cancellationToken));
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            return Ok(await favoriteService.RemoveAsync(id, cancellationToken));
        }

        [HttpGet("MostLiked")]
        public async Task<IActionResult> GetPopularAsync(CancellationToken cancellationToken = default)
        {
            return Ok(await favoriteService.GetMostLikedMedicationsAsync(cancellationToken));
        }

    }
}
