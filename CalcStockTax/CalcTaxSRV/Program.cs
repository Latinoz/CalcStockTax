using CalcTaxSRV.Services;

//ToDo: Сделать через сервис - как в DbSRV
RabbitMQListenSrv rabbit = new RabbitMQListenSrv();

rabbit.Receive();