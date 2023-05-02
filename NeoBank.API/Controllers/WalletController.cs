using BusinessDomain.Wallet;
using BusinessModel.Card;
using BusinessModel.Common;
using BusinessModel.Wallet;
using DataAccess.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace NeoBank.API.Controllers
{
    [Authorize]
    [Route("api/Wallet")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        public const string lstrFolderName = "Wallet Controller";
        Log objLog = new Log();
        private readonly IWalletBusiness _walletBusiness;
        private readonly IWalletDA _walletDA;
        public WalletController(IWalletBusiness walletBusinessInstance, IWalletDA walletDAInstance) =>
        (_walletBusiness, _walletDA) = (walletBusinessInstance, walletDAInstance);
       
        [HttpPost]
        [Route("LoadCustomerWalletByCard")]
        public IActionResult LoadCustomerWalletByCard(LoadAPIRequest loadWalletRequest)
        {
            try
            {
                objLog.WriteAppLog(" Wallet LoadCustomerWalletByCard - Response From Transcorp  :", lstrFolderName);
                var result = _walletBusiness.LoadCustomerWalletByCard(loadWalletRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                objLog.WriteAppLog(" Wallet LoadCustomerWalletByCard -  error :" + ex.Message, lstrFolderName);
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("LoadCustomerWallet")]
        public IActionResult LoadCustomerWallet(LoadWalletRequest loadWalletRequest)
        {
            try
            {
                var result = _walletBusiness.LoadCustomerWallet(loadWalletRequest);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("RefundCustomerWallet")]
        public IActionResult RefundCustomerWallet(RefundWalletRequest refundWalletRequest)
        {
            try
            {
                var result = _walletBusiness.RefundCustomerWallet(refundWalletRequest);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("FetchCustomerBalance")]
        public IActionResult FetchCustomerBalance(string entityId)
        {
            try
            {
                var result = _walletBusiness.FetchCustomerBalance(entityId);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("FetchTransactionStatus")]
        public IActionResult FetchTransactionStatus(string extTrxId)
        {
            try
            {
                var result = _walletBusiness.FetchTransactionStatus(extTrxId);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("BlockCard")]
        public IActionResult BlockCard(BlockCardRequest blockCardRequest)
        {
            try
            {
                objLog.WriteAppLog("Wallet BlockCard - Request From Transcorp  :", lstrFolderName);
                var result = _walletBusiness.BlockCard(blockCardRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                objLog.WriteAppLog("Wallet BlockCard -  error :" + ex.Message, lstrFolderName);
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("CardReplacement")]
        public IActionResult CardReplacement(CardReplacementRequest cardReplacementRequest)
        {
            try
            {
                var result = _walletBusiness.CardReplacement(cardReplacementRequest);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("GetCardDetails")]
        public IActionResult GetCardDetails(CardModelRequest EntityID)
        {
            try
            {
                var result = _walletBusiness.GetCardDetails(EntityID);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("GetCardCVVDetails")]
        public IActionResult GetCardCVVDetails(GetCVVCardModelRequest cardDetails)
        {
            try
            {
                var result = _walletBusiness.GetCardCVVDetails(cardDetails);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("FetchTransactionsByDate")]
        public IActionResult FetchTransactionsByDate(FetchTransactionsbyDatesResult trnResult)
        {
            try
            {
                var result = _walletBusiness.FetchTransactionsByDates(trnResult);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("FetchTransactions")]
        public IActionResult FetchTransactions(string extTrxId)
        {
            try
            {
                var result = _walletBusiness.FetchTransactions(extTrxId);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("PaymentForMerchant")]
        public IActionResult PaymentForMerchant(MerchantPaymentRequest merchantPaymentRequest)
        {
            try
            {
                var result = _walletBusiness.PaymentForMerchant(merchantPaymentRequest);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        [Route("AvailableNewCards")]
        public IActionResult AvailableNewCards()
        {
            try
            {
                var result = _walletDA.AvailableNewCards();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

    }
}
