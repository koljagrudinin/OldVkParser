//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using ClassLibraryVkontakteChecker.Results;
//using System.Threading.Tasks;

//namespace ClassLibraryPlanner.Data
//{
//    //class workerWithGroupsAndOptions
//    //{
//    //    dbSaver a = new dbSaver();//сохранялка в БД
//    //    //ClassLibraryVkontakteChecker.WorkClasses.MainClass vkWorker;//работник получающий данные
//    //    int tokenId;//токен
//    //    groupWithTimeOuts[] groups;//группы с таймерами

//    //    List<ClassLibraryVkontakteChecker.WorkClasses.MainClass> vkWorkers = new List<ClassLibraryVkontakteChecker.WorkClasses.MainClass>();

//    //    System.Threading.Timer timerOfChecking;

//    //    public workerWithGroupsAndOptions( Model.TokenList token )
//    //    {
//    //        foreach ( var group in token.Groups )
//    //        {
//    //            vkWorkers.Add( new ClassLibraryVkontakteChecker.WorkClasses.MainClass( token.Token , token.SecretKey , group.GroupId ) );
//    //        }
//    //        tokenId = token.Id;
//    //        this.checkTimers();



//    //        new System.Threading.Thread( this.checkTimers ).Start();
//    //    }

//    //    public void runAll( )
//    //    {
//    //        lock ( groups )
//    //        {
//    //            foreach ( var group in groups )
//    //            {
//    //                new System.Threading.Thread( RunOneGetting ).Start( group );//запускаем асинхронно
//    //            }
//    //        }
//    //    }

//    //    /// <summary>
//    //    ///	Запускаем получение данных
//    //    /// </summary>
//    //    /// <param name="groupObject"></param>
//    //    void RunOneGetting( object groupObject )
//    //    {
//    //        groupWithTimeOuts group = (groupWithTimeOuts)groupObject;


//    //        System.Threading.

//    //        vkWorker.GetListsAsync( group.group_id );
//    //        vkWorker.getBasicMetricks( group.group_id );
//    //    }

//    //    /// <summary>
//    //    /// проверка времени для всех групп
//    //    /// </summary>
//    //    void checkTimers( )
//    //    {
//    //        while ( true )
//    //        {
//    //            using ( var cont = new Model.ModelVkontakteContainer() )
//    //            {
//    //                lock ( groups )
//    //                {
//    //                    var token = cont.TokenListEnt.Single( i => i.Id == tokenId );
//    //                    groups = token.Groups.Select( i => new groupWithTimeOuts
//    //                    {
//    //                        group_id = i.GroupId ,
//    //                        BasicRegilarReading = i.BasicMetrickRegularReading ,
//    //                        BasicTimeOut = i.BasicMetricksTimeOut ,
//    //                        ListsRegularReading = i.ListsRegulerReading ,
//    //                        ListsTimeOut = i.ListsTimeOut
//    //                    } ).ToArray();
//    //                }
//    //            }
//    //            System.Threading.Thread.Sleep( 10 * 60 * 1000 );
//    //        }
//    //    }

//    //    /// <summary>
//    //    /// Реакция на полученный ListsResult
//    //    /// </summary>
//    //    void vkWorker_haveLists_EventHandler( ListsResult result )
//    //    {
//    //        a.SaveListsResultToDB( result.GroupId , result , DateTime.Now );
//    //    }

//    //    /// <summary>
//    //    /// Реация на полученный BasicResult
//    //    /// </summary>
//    //    void vkWorker_haveBasicResult_EventHandler( BasicMetricsResult result )
//    //    {
//    //        a.SaveBasicMetricksToDB( result.group_id , result , DateTime.Now );
//    //    }
//    //}
//    class groupWithTimeOuts
//    {
//        public string group_id;
//        public long ListsRegularReading;
//        public long ListsTimeOut;
//        public long BasicRegilarReading;
//        public long BasicTimeOut;

//        System.Timers.Timer timerOfCheckLists;

//        System.Timers.Timer timerOfCheckBasic;

//        System.Timers.Timer timerOfUpdateOptions;

//        List<ClassLibraryVkontakteChecker.WorkClasses.MainClass> arrOfClasses;

//        List<Task<ListsResult>> arrOfListsResultTasks = new List<Task<ListsResult>>();

//        List<Task<BasicMetricsResult>> arrOfBasicResultTasks = new List<Task<BasicMetricsResult>>();

//        public groupWithTimeOuts( Model.Groups group )
//        {
//            this.group_id = group.GroupId;

//            foreach(var 



//            initializeTimers();
//        }

//        void initializeTimers( )
//        {
//            timerOfCheckLists = new System.Timers.Timer( ListsRegularReading );
//            timerOfCheckLists.Elapsed += new System.Timers.ElapsedEventHandler( timerOfCheckLists_Elapsed );
//            timerOfCheckBasic = new System.Timers.Timer( BasicRegilarReading );
//            timerOfCheckBasic.Elapsed += new System.Timers.ElapsedEventHandler( timerOfCheckBasic_Elapsed );
//            timerOfUpdateOptions = new System.Timers.Timer( 10 * 60 * 1000 );
//            timerOfUpdateOptions.Elapsed += new System.Timers.ElapsedEventHandler( timerOfUpdateOptions_Elapsed );

//            timerOfCheckLists.Start();
//            timerOfCheckBasic.Start();
//            timerOfUpdateOptions.Start();
//        }

//        void timerOfUpdateOptions_Elapsed( object sender , System.Timers.ElapsedEventArgs e )
//        {
//            using ( var cont = new Model.ModelVkontakteContainer() )
//            {
//                var group = cont.GroupsEnt.Single( i => i.GroupId.Equals( group_id ) );

//                this.ListsRegularReading = group.ListsRegulerReading;
//                this.ListsTimeOut = group.ListsTimeOut;
//                this.BasicRegilarReading = group.BasicMetrickRegularReading;
//                this.BasicTimeOut = group.BasicMetricksTimeOut;
//            }
//            timerOfCheckBasic.Stop();
//            timerOfCheckBasic.Interval = BasicRegilarReading;
//            timerOfCheckBasic.Start();

//            timerOfCheckLists.Stop();
//            timerOfCheckLists.Interval = ListsRegularReading;
//            timerOfCheckLists.Start();
//        }

//        void timerOfCheckBasic_Elapsed( object sender , System.Timers.ElapsedEventArgs e )
//        {
//            throw new NotImplementedException();

//            foreach ( var a in arrOfClasses )
//            {
//                var getBasic = new Task<BasicMetricsResult>( a.getBasicMetricks );
//                getBasic.Wait( (int)BasicTimeOut );
//            }
//        }

//        void timerOfCheckLists_Elapsed( object sender , System.Timers.ElapsedEventArgs e )
//        {
//            throw new NotImplementedException();
//        }

//    }

//    class VkWorkerWithTasks
//    {
//        public delegate void haveBasickMetricks( Results.BasicMetricsResult result );
//        public event haveBasickMetricks haveBasicResult_EventHandler;

//        public delegate void haveLists( Results.ListsResult result );
//        public event haveLists haveLists_EventHandler;

//        ClassLibraryVkontakteChecker.WorkClasses.MainClass vkWorker;
//        System.Threading.Tasks.Task<ListsResult> listsResultTask;
//        System.Threading.Tasks.Task<BasicMetricsResult> basicResultTask;

//        int listsTimeOut = 0;
//        int basicTimeOut = 0;

//        public VkWorkerWithTasks( string token , string secret , string group_id , int listsTimeOut , int basicTimeOut )
//        {
//            vkWorker = new ClassLibraryVkontakteChecker.WorkClasses.MainClass( token , secret , group_id );
//            listsResultTask = new Task<ListsResult>( vkWorker.getLists );
//            basicResultTask = new Task<BasicMetricsResult>( vkWorker.getBasicMetricks );
//            this.listsTimeOut = listsTimeOut;
//            this.basicTimeOut = basicTimeOut;
//        }

//        void getListsResult( )
//        {

//        }

//        void getBasicResult( )
//        {

//        }

//        public void StartGetLists( )
//        {
//            new System.Threading.
//        }
//    }

//    class Results
//    {
//        public ListsResult listResult;
//        public BasicMetricsResult basicResult;
//    }


//}
