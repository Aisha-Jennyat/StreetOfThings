﻿using ApplicationLogic.AppUserQueries;
using ApplicationLogic.LoginQueries;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using static AuthorizationService.Grpc.UsersGrpc;

namespace AuthorizationService.Grpc
{
    public class UserService : UsersGrpcBase
    {
        private IUserGetter _userGetter;

        private IDistanceCalcultaor _distanceCalcultaor;

        private IUserLoginInformationGetter _userLocationGetter;

        public UserService(IUserGetter userGetter, IDistanceCalcultaor distanceCalcultaor, IUserLoginInformationGetter userLocationGetter)
        {
            _userGetter = userGetter;
            _distanceCalcultaor = distanceCalcultaor;
            _userLocationGetter = userLocationGetter;
        }

        public override async Task<UsersModel> GetUsersData(UsersIdsModel request, ServerCallContext context)
        {
            var users = (await _userGetter.GetUserByIdsAsync(request.UsersIds.ToList())).ToList();
            var model = new UsersModel();

            model.Users.AddRange(users.Select(user => new UserModel
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                PictureUrl = user.PictureUrl,
                Username = user.Username,
            }));
            return model;
        } 
        
        public override async Task<UsersModelV1_1> GetUsersDataV1_1(UsersIdsModel request, ServerCallContext context)
        {
            var users = (await _userGetter.GetUserByIdsAsync(request.UsersIds.ToList())).ToList();
            var model = new UsersModelV1_1();

            model.Users.AddRange(users.Select(user => new UserModelV1_1
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                PictureUrl = user.PictureUrl,
                Username = user.Username,
                PhoneNumber = user.PhoneNumber
            }));
            return model;
        }

        public override async Task<DistancesResponse> CalculateDistance(CalculateDistanceRequest request, ServerCallContext context)
        {
            var userIds = request.UserIds.ToList();
            var theUserId = request.TheUserId;

            var distances = await _distanceCalcultaor.CalculateDistancesAsync(theUserId, userIds);

            var distanceResponse = new DistancesResponse();
            distances.ForEach(distance => distanceResponse.Distances.Add(new DistanceModel
            {
                Distance = distance.distance,
                UserId = distance.userId
            }));

            return distanceResponse;
        }

        public override async Task<UserLocationResponse> GetUserLocation(UserIdModel request, ServerCallContext context)
        {
            var userId = request.UserId;
            if(userId == null)
            {
                return new UserLocationResponse
                {
                    Latitude = null,
                    Longitude = null
                };
            }

            var location = _userLocationGetter.GetUserLocation(userId);
            return new UserLocationResponse
            {
                Longitude = location.longitude,
                Latitude = location.latitude
            };
        }

        public override async Task<UserLoginInformationResponse> GetUserLoginInformation(UserLoginInformationRequest request, ServerCallContext context)
        {
            var tokenId = request.TokenId;
            if (tokenId == null)
            {
                return new UserLoginInformationResponse
                {
                    Latitude = null,
                    Longitude = null
                };
            }

            var login = _userLocationGetter.GetUserLoginInformation(tokenId);
            return new UserLoginInformationResponse
            {
                Longitude = login.Longitude,
                Latitude = login.Latitude,
                Email = login.Email,
                Username = login.Username,
                UserId =login.UserId,
                LoggedAtUtc = Timestamp.FromDateTime(DateTime.SpecifyKind(login.LoggedAtUtc,DateTimeKind.Utc)),
                TokenId = login.TokenId
            };
        }
    }
}
