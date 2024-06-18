namespace GraafschapCollege.BlazorApp.Pages;
using GraafschapCollege.Shared.Constants;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

[Route("/employee")]
[Authorize(Roles = Roles.Employee)]
public partial class Employee
{

}