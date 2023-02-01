﻿using ClosedXML.Web.Models.DTO;

namespace ClosedXML.Web.Models.ResponseModel
{
    public class CovidListResponseModel : _BaseResponse<_BasePagingResponse<List<CovidListDto>>>
    {
        public CovidListResponseModel():base("")
        {

        }
        public CovidListResponseModel(_BasePagingResponse<List<CovidListDto>> dto) : base(dto)
        {
        }

        public CovidListResponseModel(string message) : base(message)
        {
        }

        public CovidListResponseModel(_BasePagingResponse<List<CovidListDto>> dto, string message) : base(dto, message)
        {
        }

        public CovidListResponseModel(_BasePagingResponse<List<CovidListDto>> dto, string message, bool IsSucces) : base(dto, message, IsSucces)
        {
        }

        public CovidListResponseModel(_BasePagingResponse<List<CovidListDto>> dto, string message, bool IsSucces, string ResultCode) : base(dto, message, IsSucces, ResultCode)
        {
        }
    }
}
