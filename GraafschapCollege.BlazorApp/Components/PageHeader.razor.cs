namespace GraafschapCollege.BlazorApp.Components;
using Microsoft.AspNetCore.Components;

public partial class PageHeader
{
    [Parameter]
    public Sizes? Size { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    public enum Sizes
    {
        H1,
        H2,
        H3
    }
}