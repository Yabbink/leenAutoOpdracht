namespace GraafschapCollege.BlazorApp.Pages;

using GraafschapCollege.Shared.Clients;
using GraafschapCollege.Shared.Responses;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

[Authorize]
[Route("/")]
public partial class Home
{
    private IEnumerable<ReservationResponse> Reservations { get; set; }

    [Inject]
    private ReservationHttpClient ReservationHttpClient { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Reservations = await ReservationHttpClient.GetReservationsAsync();
    }

    private void GotoReservation()
    {

        NavigationManager.NavigateTo("/reservation/create");
    }
}