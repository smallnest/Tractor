using System;
using System.Collections.Generic;
using System.Text;

namespace Kuaff.Tractor
{
    //程序常量
    class DefinedConstant
    {
        //时间常量
        internal const int FINISHEDONCEPAUSETIME = 1500; //每圈暂停时间
        internal const int NORANKPAUSETIME = 5000; //流局时间
        internal const int GET8CARDSTIME = 1000; //摸8张底牌的时间
        internal const int SORTCARDSTIME = 1000; //我的牌排序时间
        internal const int FINISHEDTHISTIME = 2500; //每局暂停时间
        internal const int TIMERDIDA = 100; //系统滴答

    }


    /// <summary>
    /// 命令状态，指示下一步动作
    /// </summary>
    enum CardCommands
    {
        ReadyCards, //发牌命令
        DrawCenter8Cards, //画8张底牌的命令
        WaitingForSending8Cards, //等待扣底的命令
        DrawMySortedCards,//排序我的牌的命令

        Pause,//通用暂停命令
        WaitingShowPass, //显示流局的命令
        WaitingShowBottom, //翻底牌的命令
        WaitingForSend, //等待出牌
        WaitingForMySending, //等待我出牌的命令
        DrawOnceFinished,//出完一圈后的命令
        DrawOnceRank,//出完一局后的命令
        Undefined //未定义的命令
    }

    /// <summary>
    /// 保存当前游戏状态的对象
    /// </summary>
    [Serializable]
    struct CurrentState
    {
        /// <summary>
        /// 自己当前的牌局
        /// </summary>
        internal int OurCurrentRank;
        //总轮数
        internal int OurTotalRound;

        /// <summary>
        /// 对方的牌局
        /// </summary>
        internal int OpposedCurrentRank;
        //总轮数
        internal int OpposedTotalRound;

        /// <summary>
        /// 当前的花色
        /// 未定0、红桃1、黑桃2、方块3、梅花4、无主5
        /// </summary>
        internal int Suit;

        /// <summary>
        /// 当前的庄家
        /// 未定0,自己1、对家2、西3、东4
        /// </summary>
        internal int Master;

        internal CardCommands CurrentCardCommands; //当前命令

        internal CurrentState(int ourCurrentRank, int opposedCurrentRank, int suit, int master,int ourTotalRound,int opposedTotalRound, CardCommands currentCardCommands)
        {
            OurCurrentRank = ourCurrentRank;
            OpposedCurrentRank = opposedCurrentRank;
            Suit = suit;
            Master = master;
            CurrentCardCommands = currentCardCommands;
            OurTotalRound = ourTotalRound;
            OpposedTotalRound = opposedTotalRound;
        }
    }
}
