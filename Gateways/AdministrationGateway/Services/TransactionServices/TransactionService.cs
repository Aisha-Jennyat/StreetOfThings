﻿using AutoMapper;
using CommonLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ocelot.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdministrationGateway.Services.TransactionServices
{
    public class TransactionService
    {
        private HttpClient _httpClient;

        private HttpContext _httpContext;

        private HttpClientHelpers _responseProcessor;

        private IConfiguration _configs;

        private ILogger<TransactionService> _logger;

        private UserService _userService;

        private ObjectService _objectService;

        private IMapper _mapper;

        public TransactionService(HttpClient httpClient,
            IHttpContextAccessor accessor, 
            HttpClientHelpers responseProcessor,
            IConfiguration configs,
            ILogger<TransactionService> logger,
            UserService userService,
            ObjectService objectService,
            IMapper mapper)
        {
            _httpClient = httpClient;
            _httpContext = accessor.HttpContext;
            _responseProcessor = responseProcessor;
            _configs = configs;
            _logger = logger;
            _userService = userService;
            _objectService = objectService;
            _mapper = mapper;
        }

        public async Task<CommandResult<TransactionForUserDownstreamListDto>> GetTransactions()
        {
            var request = await _responseProcessor.CreateAsync(_httpContext, HttpMethod.Get, $"{_configs["Services:Transaction"]}/api/Transaction/foruser", true, true, changeBody: null);
            try
            {
                var response = await _httpClient.SendAsync(request);
                var transactionsResult = await _responseProcessor.Process<List<TransactionUpstreamDto>>(response);
                if (!transactionsResult.IsSuccessful)
                {
                    return new CommandResult<TransactionForUserDownstreamListDto>(transactionsResult.Error);
                }

                var originalUserIds = transactionsResult.Result.Select(o => o.OwnerId)
                    .Union(transactionsResult.Result.Select(o => o.ReceiverId))
                    .Distinct()
                    .ToList();
                var users = await _userService.GetUsersAsync(originalUserIds);

                var objectsIds = transactionsResult.Result.Select(t => t.ObjectId).ToList();
                var objects = await _objectService.GetObjectsByIds(objectsIds);

                return new CommandResult<TransactionForUserDownstreamListDto>(AggregateTransactionWithObject8User(transactionsResult.Result, users, objects));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error When getting list of transactions that belog to user");
                var message = new ErrorMessage
                {
                    ErrorCode = "TRANSACTION.LIST.FORUSER.ERROR",
                    Message = "there were an error while trying to execute your request",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
                return new CommandResult<TransactionForUserDownstreamListDto>(message);
            }
        }

        public async Task<CommandResult<AllTransactionsDownstreamListDto>> GetAllTransactions()
        {
            var request = await _responseProcessor.CreateAsync(_httpContext, HttpMethod.Get, $"{_configs["Services:Transaction"]}/api/Transaction/allTranses", true, true, changeBody: null);
            try
            {
                var response = await _httpClient.SendAsync(request);
                var transactionsResult = await _responseProcessor.Process<AllTransactionsUpstreamListDto>(response);
                if (!transactionsResult.IsSuccessful)
                {
                    return new CommandResult<AllTransactionsDownstreamListDto>(transactionsResult.Error);
                }

                var originalUserIds = transactionsResult.Result.Transactions.Select(o => o.OwnerId)
                    .Union(transactionsResult.Result.Transactions.Select(o => o.ReceiverId))
                    .Distinct()
                    .ToList();
                var users = await _userService.GetUsersAsync(originalUserIds);

                var objectsIds = transactionsResult.Result.Transactions.Select(t => t.ObjectId).ToList();
                var objects = await _objectService.GetObjectsByIds(objectsIds);

                return new CommandResult<AllTransactionsDownstreamListDto>(AggregateAllTransactionWithObject8User(transactionsResult.Result, users, objects));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error When getting list of transactions that belog to user");
                var message = new ErrorMessage
                {
                    ErrorCode = "TRANSACTION.LIST.FORUSER.ERROR",
                    Message = "there were an error while trying to execute your request",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
                return new CommandResult<AllTransactionsDownstreamListDto>(message);
            }
        }

        private AllTransactionsDownstreamListDto AggregateAllTransactionWithObject8User(AllTransactionsUpstreamListDto trans, List<UserDto> users, List<TransactionObjectDto> objects)
        {
            var downstreamTransactions = _mapper.Map<AllTransactionsDownstreamListDto>(trans);

            downstreamTransactions.Transactions.ForEach(t =>
            {
                var upTrans = trans.Transactions.FirstOrDefault(tt => tt.RegistrationId == t.RegistrationId);
                t.Object = objects.Find(o => o.Id == upTrans.ObjectId);
                t.Owner = users.Find(u => u.Id.EqualsIC(upTrans.OwnerId) || u.Id.EqualsIC(upTrans.ReceiverId));
                t.Receiver = users.Find(u => u.Id.EqualsIC(upTrans.OwnerId) || u.Id.EqualsIC(upTrans.ReceiverId));
            });

            return downstreamTransactions;

        }

        private TransactionForUserDownstreamListDto AggregateTransactionWithObject8User(List<TransactionUpstreamDto> trans, List<UserDto> users, List<TransactionObjectDto> objects)
        {
            var downStreamTransactions = new List<TransactionDownstreamDto>();
            foreach (var tran in trans)
            {
                downStreamTransactions.Add(new TransactionDownstreamDto
                {
                    RegistrationId = tran.RegistrationId,
                    ReceivingId = tran.ReceivingId,
                    ReturnId = tran.ReturnId,
                    RegistredAtUtc = tran.RegistredAtUtc,
                    HourlyCharge = tran.HourlyCharge,
                    ReceivedAtUtc = tran.ReceivedAtUtc,
                    ReturenedAtUtc = tran.ReturenedAtUtc,
                    ShouldReturnAfter = tran.ShouldReturnAfter,
                    TransactionStatus = tran.TransactionStatus,
                    TranscationType = tran.TranscationType,
                    Object = objects.Find(o => o.Id == tran.ObjectId),
                    Owner = users.Find(u => u.Id.EqualsIC(tran.OwnerId) || u.Id.EqualsIC(tran.ReceiverId)),
                    Receiver = users.Find(u => u.Id.EqualsIC(tran.OwnerId) || u.Id.EqualsIC(tran.ReceiverId))
            });
            }
            var userId = _httpContext.Request.Query["userId"].GetValue();
            return new TransactionForUserDownstreamListDto()
            {
                Transactions = downStreamTransactions,
                OtherUsersReservationsCount = downStreamTransactions.Count(t => t.Owner.Id.EqualsIC(userId)),
                TheUserReservationsCount = downStreamTransactions.Count(t => t.Receiver.Id.EqualsIC(userId))
            };
        }
    }
}
