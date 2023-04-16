using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var readinessDuration = TimeSpan.FromMilliseconds(1000);
var jsonString = JsonSerializer.Serialize(new { readinessDuration });

await Task.Delay(10000);
File.WriteAllText("data.json", jsonString);
await Task.Delay(10000);

var app = builder.Build();

app.MapGet("/", () => Results.Ok());

app.MapGet("/set-readiness", (int seconds) => {
    readinessDuration = TimeSpan.FromSeconds(seconds);
    return Results.Ok(readinessDuration);
});

app.MapGet("/check-readiness", async () => {
    await Task.Delay(readinessDuration);
    return Results.Ok(readinessDuration);
});

await app.RunAsync();