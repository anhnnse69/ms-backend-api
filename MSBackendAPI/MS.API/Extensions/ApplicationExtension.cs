namespace MS.API.Extensions;

public static class ApplicationExtension
{
    public static void UseApiServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowFrontend");
        app.UseMiddleware<GlobalExceptionMiddleware>();
        app.UseMiddleware<ForbiddenMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}