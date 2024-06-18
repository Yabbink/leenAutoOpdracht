namespace GraafschapCollege.BlazorApp.Pages;
using GraafschapCollege.Shared.Constants;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

[Route("/administrator")]
[Authorize(Roles = Roles.Administrator)]
public partial class Administrator
{

}