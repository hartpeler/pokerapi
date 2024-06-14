using System;
namespace th_poker_api.Common
{
    public class MessageCodes
    {
        //Please refer to developer.mozila.org/en-US/Web/HTTP/Status for further details
        public int continueMessage = 100;
        public int switchProtocol = 101;
        public int noResponseAfterReceive = 102;
        public int preloading = 103;
        public int ok = 200;
        public int upsertCompleted = 201;
        public int accepted = 202;
        public int localloaded = 203;
        public int nocontent = 204;
        public int resetcontent = 205;
        public int partialcontent = 206;
        public int multisources = 207;
        public int duplicate = 226;
        public int multiResponse = 300;
        public int reset = 301;
        public int found = 302;
        public int seeOther = 303;
        public int failToEdit = 304;
        public int proxy = 305;
        public int redirect = 307;
        public int permaRedirect = 308;
        public int error = 400;
        public int unauthorized = 401;
        public int paymentReq = 402;
        public int forbidden = 403;
        public int notfound = 404;
        public int noMethod = 405;
        public int proxyReq = 407;
        public int RTO = 408;
        public int conflict = 409;
        public int gone = 410;
        public int overloaded = 429;
        public int lengthRequired = 411;
        public int wrongCondition = 412;
        public int urltoolong = 414;
        public int wrongMedia = 415;
        public int rangeWrong = 416;
        public int serverError = 500;
        public int badGateway = 502;
        public int gatewayTimeout = 504;
    }
}

