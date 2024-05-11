namespace WsServer.Common
{
    public enum webSocketState
    {
        None = 0,
        Connecting  = 1,
        Open = 2,
        CloseSent = 3,
        CloseRecieved = 4,
        Closed = 5,
        Aborted = 6
    }
}
