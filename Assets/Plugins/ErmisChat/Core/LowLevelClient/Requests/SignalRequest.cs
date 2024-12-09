using Ermis.Core.Helpers;
using Ermis.Core.InternalDTO.Requests;
using Ermis.Core.LowLevelClient.Events;

namespace Ermis.Core.LowLevelClient.Requests
{
    public class SignalRequest : RequestObjectBase, ISavableTo<SignalRequestInternalDTO>
    {
        public string Cid;
        public string ConnectionId;
        public int Action;
        public Signal Signal;

        SignalRequestInternalDTO ISavableTo<SignalRequestInternalDTO>.SaveToDto() =>
            new SignalRequestInternalDTO
            {
                Cid = Cid,
                ConnectionId = ConnectionId,
                Action = Action,
                Signal=Signal.TrySaveToDto()
            };
    }

    public class Signal: RequestObjectBase, ISavableTo<SignalInternalDTO>
    {
       
        public string Type{ get; set; }
        
        public string Sdp { get; set; }

        SignalInternalDTO ISavableTo<SignalInternalDTO>.SaveToDto() =>
           new SignalInternalDTO
           {
               Type = Type,
               Sdp= Sdp
           };
    }
}