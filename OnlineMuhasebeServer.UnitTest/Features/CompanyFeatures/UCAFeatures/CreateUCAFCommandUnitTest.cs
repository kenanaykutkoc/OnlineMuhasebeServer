﻿using Moq;
using OnlineMuhasebeServer.Application.Features.CompanyFeatures.UCAFFeatures.Commands.CreateUCAF;
using OnlineMuhasebeServer.Application.Services;
using OnlineMuhasebeServer.Application.Services.CompanyServices;
using OnlineMuhasebeServer.Domain.CompanyEntities;
using Shouldly;

namespace OnlineMuhasebeServer.UnitTest.Features.CompanyFeatures.UCAFeatures;

public sealed class CreateUCAFCommandUnitTest
{
    private readonly Mock<IUCAFService> _ucafService;
    private readonly Mock<IApiService> _apiService;
    private readonly Mock<ILogService> _logService;

    public CreateUCAFCommandUnitTest()
    {
        _ucafService = new();
        _apiService = new();
        _logService = new();
    }

    [Fact]
    public async Task UCAFShouldBeNull()
    {
        string companyId = "585985c0-4576-4d62-ae67-59a6f72ae906";
        string code = "100.01.001";
        UniformChartOfAccount ucaf = await _ucafService.Object.GetByCodeAsync(companyId, code, default);
        ucaf.ShouldBeNull();
    }

    [Fact]
    public async Task CreateUCAFCommandResponseShouldNotBeNull()
    {
        var command = new CreateUCAFCommand(
            Code: "100.01.001",
            Name: "TL Kasa",
            Type: "M",
            CompanyId: "585985c0-4576-4d62-ae67-59a6f72ae906");

        var handler = new CreateUCAFCommandHandler(_ucafService.Object,_logService.Object,_apiService.Object);

        CreateUCAFCommandResponse response = await handler.Handle(command, default);
        response.ShouldNotBeNull();
        response.Message.ShouldNotBeEmpty();
    }
}
