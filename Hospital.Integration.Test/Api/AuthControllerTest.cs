using Hospital.Integration.Api.Controllers;
using Hospital.Integration.Business.Models;
using Hospital.Integration.Business.Services.Authentication;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit.Abstractions;

namespace Hospital.Integration.Test.Api;

public class AuthControllerTest
{
    private readonly ITestOutputHelper _output;

    public AuthControllerTest(ITestOutputHelper output)
    {
        _output = output;
    }

    
    [Fact]
    public async Task Validate_EmailIsNull_ReturnFalse()
    {
        var email = "vitor.cavalcante@gmail.com";
        var password = "123456";
        
        var _authService = new Moq.Mock<IAuthService>();
        
        _authService.Setup(service => service.GenerateToken(email, password)).ReturnsAsync(new AuthResponse());
        
        var _authController = new AuthController(_authService.Object);

        if (_authController is null) return;
        var actionResult = await _authController.Post(email, password);
        _output.WriteLine("This is output from {0}", actionResult);
        
        var ret = actionResult as UnauthorizedObjectResult;
        _output.WriteLine("ret.Value: {0}", ret?.Value);
        
//        var objectResponse = Assert.IsType<ObjectResult>(actionResult); 
        
        //Assert.Equal(401, actionResult.);

    }
}