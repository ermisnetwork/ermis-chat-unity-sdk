##  Table of contents

1. [Introduction](https://docs.ermis.network/JavaScript/doc#introduction)
2. [Requirements](https://docs.ermis.network/JavaScript/doc#requirements)
3. [Getting Started](https://docs.ermis.network/JavaScript/doc#getting-started)
4. [Features](https://docs.ermis.network/JavaScript/doc#features)
5. [Error codes](https://docs.ermis.network/JavaScript/doc#error-codes)

## Introduction

The ErmisChat SDK for JavaScript allows you to integrate real-time chat into your client app with minimal effort.

## [](https://docs.ermis.network/JavaScript/doc#requirements)Requirements

This section shows you the prerequisites needed to use the ErmisChat SDK for Unity. If you have any comments or questions regarding bugs and feature requests, please reach out to us.

## [](https://docs.ermis.network/JavaScript/doc#supported-browsers)Supported browsers

| Browser           | Supported versions     |
| ----------------- | ---------------------- |
| Internet Explorer | Not supported          |
| Edge              | 13 or higher           |
| Chrome            | 16 or higher           |
| Firefox           | 11 or higher           |
| Safari            | 7 or higher            |
| Opera             | 12.1 or higher         |
| iOS Safari        | 7 or higher            |
| Android Browser   | 4.4 (Kitkat) or higher |

## [](https://docs.ermis.network/JavaScript/doc#getting-started)Getting started

The ErmisChat client is designed to allow extension of the base types through use of generics when instantiated. By default, all generics are set to `Record<string, unknown>`.

## [](https://docs.ermis.network/JavaScript/doc#step-by-step-guide)Step-by-Step Guide:

### [](https://docs.ermis.network/JavaScript/doc#step-1-generate-api-key-and-projectid)Step 1: Generate API key and ProjectID

Before installing ErmisChat SDK, you need to generate an **API key** and **ProjectID** on the [Ermis Dashboard](https://ermis.network/). This **API key** and **ProjectID** will be required when initializing the Chat SDK.

> **Note**: Ermis Dashboard will be available soon. Please contact our support team to create a client account and receive your API key. Contact support: [tony@ermis.network](mailto:tony@ermis.network)

### [](https://docs.ermis.network/JavaScript/doc#step-2-install-chat-sdk)Step 2: Install Chat SDK[]

You can install the Chat SDK with newest package here: 

### Step 3: Demo

Open scene ChatDemo from Plugins\ErmisChat\SampleProject\Scenes. Run Scene:

<img src="C:\Users\plkho\AppData\Roaming\Typora\typora-user-images\image-20241210032214978.png" alt="image-20241210032214978" style="zoom:50%;" />





### Step 4: Integrate Login via Wallet

You need to import `ErmisAuth` from Ermis to connect to the login flow in Ermis Chat:

Step 4: Integrate Login via Wallet

> [](https://docs.ermis.network/JavaScript/doc#4-2-sign-wallet-and-get-token)4.2 Sign wallet and Get Token

Retrieve the token:

**Example**:

```javascript
 var tokenProvider = ErmisChatClient.CreateDefaultTokenProvider(TokenUriHandle);
 var _userToken = await tokenProvider.GetTokenAsync(_apiKey, _tokenHeader);
 _tokenResponeDto = JsonConvert.DeserializeObject<TokenResponeDto>(_userToken);
 callback?.Invoke();
 
```

**Response**

```javascript
{
  "token": "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoiMHg4ZWI3MTgwMzNiNGEzYzVmOGJkZWExNzczZGVkMDI1OWIyMzAwZjVkIiwiY2xpZW50X2lkIjoiNmZiZGVjYjAtMWVjOC00ZTMyLTk5ZDctZmYyNjgzZTMwOGI3IiwiY2hhaW5faWQiOjAsInByb2plY3RfaWQiOiJiNDQ5MzdlNC1jMGQ0LTRhNzMtODQ3Yy0zNzMwYTkyM2NlODMiLCJhcGlrZXkiOiJrVUNxcWJmRVF4a1pnZTdISERGY0l4Zm9IenFTWlVhbSIsImVybWlzIjp0cnVlLCJleHAiOjE4MjU1MzQ4MjI2NDMsImFkbWluIjpmYWxzZSwiZ2F0ZSI6ZmFsc2V9.nP2pIx1PAG-GrjNPgh8pJNfMfL-rX8YFpsDB-yFKjQs",
  "refresh_token": "Aeqds63dfXXKqGkUrgsS6K2O",
  "user_id": "0x8eb718033b4a3c5f8bdea1773ded0259b2300f5d",
  "project_id": "b44937e4-c0d4-4a73-847c-3730a923ce83"
}
```

### [](https://docs.ermis.network/JavaScript/doc#step-5-initialize-the-chat-sdk)Step 5: Initialize the Chat SDk

On the client-side, initialize the Chat client with your **API key** and **ProjectID**.



```javascript
 Client = ErmisChatClient.CreateDefaultClient(config);

```

Once initialized, you must specify the current user with `connectUser`. We provide methods to initialize the client:

#### 

```javascript
 AuthCredentials authCredentials = new AuthCredentials(_apiKey, _projectId, token);
 _client.ConnectUserAsync(authCredentials);
```

| Name         | Type   | Required | Description                                                  |
| ------------ | ------ | -------- | ------------------------------------------------------------ |
| `_apiKey`    | string | Yes      | Api key on Ermis Dashboard                                   |
| `_projectId` | string | Yes      | ProjectId on Ermis Dashboard                                 |
| token        | string | Yes      | Authentication token obtained from the `GetTokenAsync` function |

### [](https://docs.ermis.network/JavaScript/doc#1-sign-method-via-wallet)2. Sending your first message

Now that the Chat SDK has been imported, you’re ready to start sending messages. Here are the steps to send your first message using the Chat SDK:

**Send a message to the channel**:



```javascript
 var sendMessageRequest = new ErmisSendMessageRequest
 {
     Text = "Hello",
     Id = Guid.NewGuid().ToString()
 };
ermisChannel.SendNewMessageAsync(sendMessageRequestt);
```

## [](https://docs.ermis.network/JavaScript/doc#features)Features

1. [User management](https://docs.ermis.network/JavaScript/doc#user-management)
2. [Channel management](https://docs.ermis.network/JavaScript/doc#channel-management)
3. [Message management](https://docs.ermis.network/JavaScript/doc#message-management)
4. [Events](https://docs.ermis.network/JavaScript/doc#events)

### [](https://docs.ermis.network/JavaScript/doc#user-management)User management

Get the users in your project to create a direct message.

#### [](https://docs.ermis.network/JavaScript/doc#1-query-users)1. Query users



```javascript
 var users = await Client.QueryUsersListAsync();
 foreach (var user in users)
 {
     _ermisUsers.Add(user);
 }
```

**Response**

```javascript
{
  "data": [
      {
        "id": "0x9add536fb802c3eecdb2d94a29653e9b42cc4291",
        "name": "0x9add536fb802c3eecdb2d94a29653e9b42cc4291",
        "avatar": null,
        "about_me": null,
        "project_id": "b44937e4-c0d4-4a73-847c-3730a923ce83"
      },
      {
        "id": "0x360a45f70de193090a1b13da8393a02f9119aecd",
        "name": "vinhtc27",
        "avatar": "https://hn.storage.weodata.vn/namwifi/ermis/staging/wLdIngOpu8j9mp49oOhwWOzQyO31qjLK",
        "about_me": null,
        "project_id": "b44937e4-c0d4-4a73-847c-3730a923ce83"
      },
  ],
  "count": 8,
  "total": 8,
  "page": 1,
  "page_count": 1
}
```

### [](https://docs.ermis.network/JavaScript/doc#channel-management)Channel management

#### [](https://docs.ermis.network/JavaScript/doc#1-query-channels)1. Query channels

Here’s an example of how to query the list of channels:



```javascript
GetChannelsRequest request = new GetChannelsRequest();
request.FilterConditions = new FilterConditions();
request.FilterConditions.Type = new List<string>();
request.FilterConditions.Type.Add("general");
request.FilterConditions.Type.Add("team");
request.FilterConditions.Type.Add("messaging");
request.FilterConditions.Limit = null;
request.FilterConditions.Offset = 0;
request.FilterConditions.Roles = new List<string>();
request.FilterConditions.Roles.Add("owner");
request.FilterConditions.Roles.Add("moder");
request.FilterConditions.Roles.Add("member");
request.FilterConditions.OtherRoles = new List<string>();
//requestBodyDto.FilterConditions.OtherRoles.Add("pending");
request.FilterConditions.Banned = false;
request.FilterConditions.Blocked = false;
request.FilterConditions.ProjectId = Client.LowLevelClient.GetProjectId();
request.Sort = new List<Sort>();
Sort _sort = new Sort();
_sort.Field = "last_message_at";
_sort.Direction = -1;
request.Sort.Add(_sort);
request.MessageLimit = 25;

var channels = await Client.QueryChannelsAsync(request);


```

**Filter:** Type: Object. The query filters to use. You can filter by any custom fields you’ve defined on the Channel.

| Name        | Type    | Required | Description                                                  |
| ----------- | ------- | -------- | ------------------------------------------------------------ |
| type        | array   | No       | The type of channel: messaging, team. If the array is empty, it will return all channels. |
| roles       | array   | No       | This method is used to retrieve a list of channels that the current user is a part of. The API supports filtering channels based on the user’s role within each channel, including roles such as `owner`, `moder`, `member`, and `pending`.  `owner` - Retrieves a list of channels where the user’s role is the owner. `moder` - Retrieves a list of channels where the user’s role is the moderator. `member` - Retrieves a list of channels where the user’s role is a member. `pending` - Retrieves a list of channels where the user’s role is pending approval. |
| other_roles | array   | No       | This API allows you to retrieve a list of channels that you have created, with the ability to filter channels based on the roles of other users within the channel. The roles available for filtering include: `owner`, `moder`, `member`, and `pending`.  `owner` - Filter channels where the user is the channel owner. `moder` - Filter channels where the user is a moderator. `member` - Filter channels where the user is a member. `pending` - Filter channels where the user is pending approval. |
| blocked     | boolean | No       | Filter channels based on the `blocked` boolean field. If `true`, filter blocked direct channels. If `false`, filter non-blocked channels. If not provided, filter all channels. |
| limit       | integer | No       | The maximum number of channels to retrieve in a single request. |
| offset      | integer | No       | The starting position for data retrieval. This parameter allows you to retrieve channels starting from a specific position, useful for paginating through results. For example, offset: 30 will start retrieving channels from position 31 in the list. |

**Sort:** Type: Object or array of objects. The sorting used for the channels that match the filters. Sorting is based on the field and direction, and multiple sorting options can be provided. You can sort based on fields such as `last_message_at`. Direction can be ascending (1) or descending (-1).



```javascript
const sort = [{ last_message_at: -1 }];
```

**Options:** Type: Object. This method can be used to fetch information about existing channels, including message counts and other related details.

| Name          | Type    | Required | Description                                                  |
| ------------- | ------- | -------- | ------------------------------------------------------------ |
| message_limit | integer | No       | The maximum number of messages to retrieve from each channel. If this parameter is not provided, the default number of messages or no limit will be applied. |



```javascript
const options = { message_limit: 25 };
```

#### [](https://docs.ermis.network/JavaScript/doc#2-create-a-new-channel)2. Create a New Channel

To create a channel: choose Direct for 1-1 (messaging) or Channel (team) for multiple users.

**New direct message**



```javascript
// channel type is messaging
Client.CreateChannelWithMembersAsync(ChannelType.Messaging, user);
```

| Name | Type  | Required | Description                                                  |
| ---- | ----- | -------- | ------------------------------------------------------------ |
| user | array | Yes      | an array with two user IDs: the creator’s user ID and the recipient’s user ID |

**New channel**



```javascript
// channel type is team
Client.CreateChannelWithIdAsync(ChannelType.Team, channelId: Guid.NewGuid().ToString(),channelName, users);
```

| Name | Type   | Required | Description                                      |
| ---- | ------ | -------- | ------------------------------------------------ |
| name | string | Yes      | Display name for the channel                     |
| user | array  | Yes      | List user id you want to adding for this channel |
|      |        |          |                                                  |

> **Note**: The channel is created, allowing only the creator’s friends to be added, maintaining security and connection.

#### [](https://docs.ermis.network/JavaScript/doc#3-accept-reject-invite)3. Accept/Reject Invite

**Accept the invitation**



```javascript
await ermisChannel.AcceptInviteAsync();
```

| Name   | Type   | Required | Description                                                  |
| ------ | ------ | -------- | ------------------------------------------------------------ |
| action | string | Yes      | If `accept` to approve an invite. If `join` to enter a public channel via a public link |

**Reject the invitation**



```javascript
await ermisChannel.RejectInviteAsync()
```

#### [](https://docs.ermis.network/JavaScript/doc#5-setting-a-channel)4. Setting a channel

The channel settings feature allows users to customize channel attributes such as name, description, membership permissions, and notification settings to suit their communication needs.

**5.1. Edit channel information (name, avatar, description)**



```javascript
await Client.UpdateChannelAsync(channelType,channelId, new UpdateChannelRequest());
```

| Name        | Type   | Required | Description                  |
| ----------- | ------ | -------- | ---------------------------- |
| name        | string | No       | Display name for the channel |
| image       | string | No       | Avatar for the channel       |
| description | string | No       | Description for the channel  |

**5.2. Adding, Removing Channel Members** The addMembers() method adds specified users as members, while removeMembers() removes them.

**Adding members** List user id you want to adding



```javascript
await ermisChannel.AddMembersAsync(users);
```

**Removing members** List user id you want to removing



```javascript
await ermisChannel.RemoveMembersAsync(users);
```

**5.3. Adding & Removing Moderators to a Channel** The addModerators() method adds a specified user as a Moderators (or updates their role to moderator if already members), while demoteModerators() removes the moderator status.

**Adding Moderator** List user id you want to adding



```javascript
await ermisChannel.PromoteMembersAsync(users);
```

**Removing Moderator** List user id you want to removing



```javascript
await ermisChannel.DemoteMembersAsync(users);
```

**5.4. Ban & Unban Channel Members** The ban and unban feature allows administrators to block or unblock members with the “member” role in a channel, managing their access rights.

**Ban a Channel Member** List user id you want to ban



```javascript
await ermisChannel.BanMembersAsync(users);
```

**Unban a Channel Member** List user id you want to unban



```javascript
await ermisChannel.UnBanMembersAsync(users);
```

**5.5. Channel Capabilities** This feature allows `owner` role to configure permissions for members with the `member` role, enabling a capability adds it to the capabilities, disabling it removes it from the capabilities.



```javascript
await ermisChannel.UpdateChannelCapabilitiesAsync(new UpdateChannelCapabilitiesRequest())
```

| Name         | Type  | Required | Description                                                  |
| ------------ | ----- | -------- | ------------------------------------------------------------ |
| capabilities | array | Yes      | Capabilities you want to adding to the member in channel. Enabling a capability adds it to the array, disabling it removes it from the array |

**Name Capabilities** These are the permissions applied to members within a channel.

| Name                  | What it indicates                               |
| --------------------- | ----------------------------------------------- |
| `send-message`        | Ability to send a message                       |
| `update-own-message`  | Ability to update own messages in the channel   |
| `delete-own-message`  | Ability to delete own messages from the channel |
| `send-reaction`       | Ability to send reactions                       |
| `create-call`         | Ability to create call in the channel           |
| `join-call`           | Ability to join call in the channel             |
| `send-links`          | Ability to send links messages in the channel   |
| `quote-message`       | Ability to quote message in the channel         |
| `send-reply`          | Ability to send reply message in the channel    |
| `search-messages`     | Ability to search messages in the channel       |
| `send-typing-events`  | Ability to send typing events in the channel    |
| `upload-file`         | Ability to upload file in the channel           |
| `delete-own-reaction` | Ability to delete reaction in the channel       |

**5.6. Query Attachments in a channel** This feature allows users to view all media files shared in a channel, including images, videos, and audio.



```javascript
await ermisChannel.GetAttachmentAsync();
```

**Response**



```javascript
{
  "attachments": [
    {
      "id": "3fe7e002-2c71-48bc-b051-a284825969a7",
      "user_id": "0x8eb718033b4a3c5f8bdea1773ded0259b2300f5d",
      "cid": "messaging:b44937e4-c0d4-4a73-847c-3730a923ce83:65c07c7cc7c28e32d8f797c2e13c3e02f1fd",
      "url": "https://hn.storage.weodata.vn/belochat/bellboy/test/messaging:b44937e4-c0d4-4a73-847c-3730a923ce83:65c07c7cc7c28e32d8f797c2e13c3e02f1fd/3fe7e002-2c71-48bc-b051-a284825969a7",
      "thumb_url": "",
      "file_name": "about3.png",
      "content_type": "image/png",
      "content_length": 34781,
      "content_disposition": "inline; filename=\"about3.png\"",
      "message_id": "1b1d81fd-3bfe-4ac0-ad83-4f7b99ce2252",
      "created_at": "2024-08-29T11:22:41.210527653+00:00",
      "updated_at": "2024-08-29T11:22:41.210531736+00:00"
    },
  ],
  "duration": "1ms"
}
```

**5.7. Block & Unblock a Direct channel** Allows users to block any user in their DM list. Users can unblock at any time while retaining the previous conversation history.

> **Note**: Only allows /unblock for direct channels with type `messaging`, not applicable for group channels with type `team`

**Block a Direct channel** The block direct channel feature prevents users from sending messages, triggering the `member.blocked` event via WebSocket



```javascript
 BlockUnBlockChannelRequest blockUnBlockChannelRequest =new BlockUnBlockChannelRequest
 {
     Action="block"
 }
await ermisChannel.BlockOrUnBlockAsync(blockUnBlockChannelRequest);
```

**Unblock a Direct channel** The unblock direct channel feature allows users to resume messaging, triggering the `member.unblocked` event via WebSocket.



```javascript
 BlockUnBlockChannelRequest blockUnBlockChannelRequest =new BlockUnBlockChannelRequest
 {
     Action="unblock"
 }
await ermisChannel.BlockOrUnBlockAsync(blockUnBlockChannelRequest);
```

#### [](https://docs.ermis.network/JavaScript/doc#6-search-public-channel)6. Search public channel

The public channel search feature allows users to find public channels, making it easy to connect and join open communities



```javascript
await Client.SearchPublicChannelAsync(new SearchPublicChannelRequest())
```

**Response**



```javascript
{
  "search_result": {
    "limit": 25,
    "offset": 0,
    "total": 2,
    "channels": [
      {
        "index": "a7e6d4b2-4aaa-4ee1-862a-8ecaddee8d71",
        "cid": "team:b44937e4-c0d4-4a73-847c-3730a923ce83:a7e6d4b2-4aaa-4ee1-862a-8ecaddee8d71",
        "type": "team",
        "name": "room T1 public",
        "image": "",
        "description": "hihihi",
        "created_at": "2024-11-04T10:11:40.654364Z",
        "created_by": "0x8eb718033b4a3c5f8bdea1773ded0259b2300f5d"
      },
      {
        "index": "vinh-thang-room",
        "cid": "team:b44937e4-c0d4-4a73-847c-3730a923ce83:vinh-thang-room",
        "type": "team",
        "name": "Vinh-Thang-Room",
        "image": "https://img-cdn.pixlr.com/image-generator/history/65bb506dcb310754719cf81f/ede935de-1138-4f66-8ed7-44bd16efc709/medium.webp",
        "description": "long description for this channel",
        "created_at": "2024-10-21T03:18:40.463468Z",
        "created_by": "0x360a45f70de193090a1b13da8393a02f9119aecd"
      }
    ]
  },
  "duration": "16ms"
}
```

### [](https://docs.ermis.network/JavaScript/doc#message-management)Message management

#### [](https://docs.ermis.network/JavaScript/doc#1-sending-a-message)1. Sending a message

This feature allows user to send a message to a specified channel or DM:

**1.1 Send text message**



```javascript
 var sendMessageRequest = new ErmisSendMessageRequest
 {
     Text = "Hello",
     Id = Guid.NewGuid().ToString()
 };
ermisChannel.SendNewMessageAsync(sendMessageRequestt);
```

**Response**



```javascript
{
  "message": {
    "id": "99873843-757f-4b3a-95d0-0773314fb115",
    "text": "Hello",
    "type": "regular",
    "cid": "messaging:b44937e4-c0d4-4a73-847c-3730a923ce83:65c07c7cc7c28e32d8f797c2e13c3e02f1fd",
    "user": {
      "id": "0x8eb718033b4a3c5f8bdea1773ded0259b2300f5d"
    },
    "created_at": "2024-08-29T10:44:40.022289401+00:00"
  },
  "duration": "0ms"
}
```

**1.2 Send attachments message** Before sending messages with images, videos, or file attachments, users need to [upload the files](https://docs.ermis.network/JavaScript/doc#2-upload-file) to the system for sending.



```javascript
 var sendMessageRequest = new ErmisSendMessageRequest
 {
     Text = "Hello",
     Id = Guid.NewGuid().ToString()
 };
 sendMessageRequest.Attachments = new List<ErmisAttachmentRequest>();
 ErmisAttachmentRequest request = new ErmisAttachmentRequest();
 request.Type = uploadedFileType;
 request.Title = uploadedFileTitle;
 request.FileSize = (int)uploadedFileSize;
 request.MimeType = uploadFileMime;
 if (uploadedFileType == "image")
 {
     request.ImageUrl = uploadedFileUrl;
 }
 else if (uploadedFileType == "file" || uploadedFileType == "voiceRecording")
 {
     request.AssetUrl = uploadedFileUrl;
 }
 else if (uploadedFileType == "video")
 {
     request.AssetUrl = uploadedFileUrl;
     request.ThumbUrl = "";
 }
 sendMessageRequest.Attachments.Add(request);
ermisChannel.SendNewMessageAsync(sendMessageRequestt);
```

**Response**



```javascript
{
  "message": {
    "id": "398b7c12-e412-493c-9f37-0b1d2842d339",
    "text": "",
    "type": "regular",
    "cid": "messaging:b44937e4-c0d4-4a73-847c-3730a923ce83:65c07c7cc7c28e32d8f797c2e13c3e02f1fd",
    "user": {
        "id": "0x8eb718033b4a3c5f8bdea1773ded0259b2300f5d"
    },
    "created_at": "2024-09-07T12:49:17.037397729+00:00",
    "attachments": [
      {
        "title": "photo_webclip.png",
        "file_size": 4584,
        "type": "image",
        "mime_type": "image/png",
        "image_url": "https://bit.ly/2K74TaG"
      }
    ]
  },
  "duration": "3ms"
}
```

**Attachments Format** `attachments` is an array containing objects that represent different types of attachments such as images, videos, or files. Each object has the following fields:

- `type`: The type of file (image, video, file)
- `image_url` or `asset_url`: URL of the file after uploading
- `title`: The name of the file
- `file_size`: The size of the file (in bytes)
- `mime_type`: The MIME type of the file
- `thumb_url`: Thumbnail URL (applies to videos)

**Example**



```javascript
const attachments = [
  {
    type: 'image', // Upload file image
    image_url: 'https://bit.ly/2K74TaG', // url from response upload file
    title: 'photo.png',
    file_size: 2020,
    mime_type: 'image/png',
  },
  {
    type: 'video', // Upload file video
    asset_url: 'https://bit.ly/2K74TaG', // url from response upload file
    file_size: 10000,
    mime_type: 'video/mp4',
    title: 'video name',
    thumb_url: 'https://bit.ly/2Uumxti',
  },
  {
    type: 'file', // Upload another file
    asset_url: 'https://bit.ly/3Agxsrt', // url from response upload file
    file_size: 2000,
    mime_type: 'application/msword',
    title: 'file name',
  },
];
```

**Get thumb blob from video** Extract a thumbnail from a video file, converting it to a Blob if the uploaded file is a video. After upload file



```javascript
await channel.getThumbBlobVideo(file);
```

**1.3 Reply a message** The reply feature allows users to directly respond to a specific message, displaying the original message content alongside the reply.



```javascript

 var sendMessageRequest = new ErmisSendMessageRequest
 {
     Text = "Hello",
    QuotedMessage: '99873843-757f-4b3a-95d0-0773314fb115',
 };
ermisChannel.SendNewMessageAsync(sendMessageRequestt);
```

**Response**



```javascript
{
  "message": {
    "id": "cc7d8206-0f67-4b2b-8f8d-8a721ee0a4b1",
    "text": "hehe",
    "type": "reply",
    "cid": "messaging:b44937e4-c0d4-4a73-847c-3730a923ce83:65c07c7cc7c28e32d8f797c2e13c3e02f1fd",
    "user": {
      "id": "0x8eb718033b4a3c5f8bdea1773ded0259b2300f5d"
    },
    "created_at": "2024-09-07T12:47:58.398896591+00:00",
    "quoted_message_id": "eacc4834-1b73-4eca-9108-409f1f9a91db",
    "quoted_message": {
      "id": "eacc4834-1b73-4eca-9108-409f1f9a91db",
      "text": "hello",
      "type": "regular",
      "cid": "messaging:b44937e4-c0d4-4a73-847c-3730a923ce83:65c07c7cc7c28e32d8f797c2e13c3e02f1fd",
      "user": {
        "id": "0x8eb718033b4a3c5f8bdea1773ded0259b2300f5d"
      },
      "created_at": "2024-09-06T10:27:50.361815802+00:00"
    }
  },
  "duration": "0ms"
}
```

**1.4 Send message with mentions** Allows users to mention others by typing `@`, displaying the selected name and ID.

> **Note**: Only allows send message with mentions for group channels with type `team`, not applicable for direct channels with type `messaging`

**Case with specific mentions**:



```javascript
 var sendMessageRequest = new ErmisSendMessageRequest
 {
     Text = "Hello",
    MentionedUsers= new List<IErmisUser>(),
 };
ermisChannel.SendNewMessageAsync(sendMessageRequestt);
```

**Case with mentioning everyone**:



```javascript
var sendMessageRequest = new ErmisSendMessageRequest
 {
     Text = "Hello",
     MentionedAll=true,
 };
ermisChannel.SendNewMessageAsync(sendMessageRequestt);
```

| Name            | Type    | Required | Description                                                  |
| --------------- | ------- | -------- | ------------------------------------------------------------ |
| text            | string  | Yes      | The message content, which can include mention IDs (e.g., `@mention_id_1 @mention_id_2 Hello`) |
| mentioned_all   | boolean | No       | A boolean that, if `true`, mentions all users in the channel. If `false`, only specific users in mentioned_users are mentioned |
| mentioned_users | array   | No       | An array containing the IDs of the users being mentioned in the message. Each ID in the array (e.g., `mention_id_1`, `mention_id_2`) represents a user who will receive a mention notification |

**Response**



```javascript
{
  "message": {
    "id": "99873843-757f-4b3a-95d0-0773314fb115",
    "mentioned_all": false,
    "mentioned_users": ['mention_id_1', 'mention_id_2'],
    "text": "@mention_id_1 @mention_id_2 Hello",
    "type": "regular",
    "cid": "messaging:b44937e4-c0d4-4a73-847c-3730a923ce83:65c07c7cc7c28e32d8f797c2e13c3e02f1fd",
    "user": {
      "id": "0x8eb718033b4a3c5f8bdea1773ded0259b2300f5d"
    },
    "created_at": "2024-08-29T10:44:40.022289401+00:00"
  },
  "duration": "0ms"
}
```

#### [](https://docs.ermis.network/JavaScript/doc#2-upload-file)2. Upload file

This feature allows user to upload a file to the system. Maximum file size is 2GB



```javascript
await ermisChannel.UploadFileAsync(fileContent, "attachment-1");
```

**Response**



```javascript
{
  "file": "https://hn.storage.weodata.vn/belochat/bellboy/test/team:b44937e4-c0d4-4a73-847c-3730a923ce83:ac7018e7-d398-4053-80f0-116aefc80682/5295276b-41d4-4738-b9fd-7b2f3c005a23",
  "duration": "277ms"
}
```

#### [](https://docs.ermis.network/JavaScript/doc#3-edit-message)3. Edit message

The edit message feature enables users to modify and update the content of a previously sent message in a chat



```javascript
var ErmisUpdateMessageRequest = new ErmisUpdateMessageRequest
 {
     Text = "Hello",
 };

await ermisMessage .UpdateAsync(message_id, text);
```

**Response**



```javascript
{
  "message": {
    "id": "99873843-757f-4b3a-95d0-0773314fb115",
    "text": "Hello",
    "type": "regular",
    "cid": "messaging:b44937e4-c0d4-4a73-847c-3730a923ce83:65c07c7cc7c28e32d8f797c2e13c3e02f1fd",
    "user": {
        "id": "0x8eb718033b4a3c5f8bdea1773ded0259b2300f5d"
    },
    "created_at": "2024-08-29T10:44:40.022289401+00:00"
  },
  "duration": "0ms"
}
```

#### [](https://docs.ermis.network/JavaScript/doc#4-delete-message)4. Delete message

The delete message feature allows users to remove a previously sent message from the chat for all participants



```javascript
ermisMessage.SoftDeleteAsync()
```

**Response**



```javascript
{
  "message": {
    "id": "99873843-757f-4b3a-95d0-0773314fb115",
    "text": "Hello",
    "type": "regular",
    "cid": "messaging:b44937e4-c0d4-4a73-847c-3730a923ce83:65c07c7cc7c28e32d8f797c2e13c3e02f1fd",
    "user": {
        "id": "0x8eb718033b4a3c5f8bdea1773ded0259b2300f5d"
    },
    "created_at": "2024-08-29T10:44:40.022289401+00:00"
  },
  "duration": "0ms"
}
```

#### [](https://docs.ermis.network/JavaScript/doc#7-reactions)7. Reactions

The Reaction feature allows users to send, manage reactions on messages, and delete reactions when necessary.

The message reaction feature allows users to quickly respond with five types of reactions: ‘haha’, ‘like’, ‘love’, ‘sad’, and ‘fire’.

**Example**



```javascript
const EMOJI_QUICK = [
  {
    type: 'haha',
    value: '😂',
  },
  {
    type: 'like',
    value: '👍',
  },
  {
    type: 'love',
    value: '❤️',
  },
  {
    type: 'sad',
    value: '😔',
  },
  {
    type: 'fire',
    value: '🔥',
  },
];
```

**7.1. Send a reaction:**



```javascript
ermisMessage.SendReactionAsync(reaction_type);
```

| Name          | Type   | Required | Description                   |
| ------------- | ------ | -------- | ----------------------------- |
| reaction_type | string | Yes      | ID of the message to react to |
|               |        |          |                               |

**Response**



```javascript
{
  "message": {
    "id": "b9339abe-eb4f-43a7-954b-9397bf1a77ca",
    "text": "tuan 2",
    "type": "regular",
    "cid": "messaging:b44937e4-c0d4-4a73-847c-3730a923ce83:65c07c7cc7c28e32d8f797c2e13c3e02f1fd",
    "user": {
        "id": "0x8eb718033b4a3c5f8bdea1773ded0259b2300f5d"
    },
    "created_at": "2024-08-29T10:56:47.392938048+00:00",
    "latest_reactions": [
        {
          "message_id": "b9339abe-eb4f-43a7-954b-9397bf1a77ca",
          "user_id": "0x8eb718033b4a3c5f8bdea1773ded0259b2300f5d",
          "user": {
              "id": "0x8eb718033b4a3c5f8bdea1773ded0259b2300f5d"
          },
          "type": "love",
          "created_at": "2024-08-29T11:01:04.533983699+00:00",
          "updated_at": "2024-08-29T11:01:04.533987884+00:00"
        }
    ],
    "reaction_counts": {
        "love": 1
    }
  },
  "reaction": {
    "message_id": "b9339abe-eb4f-43a7-954b-9397bf1a77ca",
    "user_id": "0x8eb718033b4a3c5f8bdea1773ded0259b2300f5d",
    "user": {
        "id": "0x8eb718033b4a3c5f8bdea1773ded0259b2300f5d"
    },
    "type": "love",
    "created_at": "2024-08-29T11:01:04.533983699+00:00",
    "updated_at": "2024-08-29T11:01:04.533987884+00:00"
  },
  "duration": "21ms"
}
```

**7.2. Delete a reaction:**



```javascript
ermisMessage.DeleteReactionAsync(reaction_type);
```

| Name          | Type   | Required | Description                   |
| ------------- | ------ | -------- | ----------------------------- |
| reaction_type | string | Yes      | ID of the message to react to |
|               |        |          |                               |

#### 
