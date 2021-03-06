#ifndef _NET_FACTORY_H_
#define _NET_FACTORY_H_

#include "net/acceptorimpl/acceptor_linux.h"
#include "net/acceptorimpl/acceptor_linux_gate.h"

#include "net/eventloopimpl/epoll.h"
#include "net/eventloopimpl/select.h"
#include "connector.h"
#include "base/singleton.h"
#include "base/perf_monitor.h"
#include "base/thread.h"
#include "net/codec.h"
#include "net/msg_sender.h"
#include "base/log.h"

namespace ff {

class NetFactory
{
public:
    struct NetData
    {
        volatile bool           started_flag;
        TaskQueuePool*          tg;
        Thread                  thread;
        #ifdef _WIN32
        Select                  epoll;
        #else
        Epoll                   epoll;
        #endif
        std::vector<Acceptor*>  allAcceptor;
        NetData():
            started_flag(false),
            tg(NULL),
            epoll()
        {
        }
        ~ NetData()
        {
            stop();
            for (size_t i = 0; i < allAcceptor.size(); ++i)
            {
                delete allAcceptor[i];
            }
            allAcceptor.clear();
            delete tg;
            tg = NULL;
        }
        static void runEpoll(void* e_)
        {
            NetData* p = (NetData*)e_;
            p->epoll.runLoop();
        }
        void start(int thread_num_ = 2)
        {
            if (false == started_flag)
            {
            	#ifdef _WIN32
            	WSADATA wsaData;
				WSAStartup(MAKEWORD(1,1),&wsaData);
            	#endif
                assert(thread_num_ > 0);
                started_flag = true;
                tg = new TaskQueuePool(thread_num_);
                thread.create_thread(Task(&runEpoll, this), 1);
                thread.create_thread(TaskQueuePool::gen_task(tg), thread_num_);
            }
        }
        void stop()
        {
            if (true == started_flag)
            {
                for (size_t i = 0; i < allAcceptor.size(); ++i)
                {
                    allAcceptor[i]->close();
                }

                tg->close();
                epoll.close();
                thread.join();

                started_flag = false;
            }
            #ifdef _WIN32
            WSACleanup( );
            #endif
        }
    };

    static int start(int thread_num_)
    {
        Singleton<NetData>::instance().start(thread_num_);
        return 0;
    }
    static int stop()
    {
        Singleton<NetData>::instance().stop();
        return 0;
    }
    static Acceptor* listen(const std::string& host_, MsgHandler* msg_handler_)
    {
        Singleton<NetData>::instance().start();
        AcceptorLinux* ret = new AcceptorLinux(&(Singleton<NetData>::instance().epoll),
                                                   msg_handler_,
                                                   (Singleton<NetData>::instance().tg));

        if (ret->open(host_))
        {
            delete ret;
            return NULL;
        }
        Singleton<NetData>::instance().allAcceptor.push_back(ret);
        return ret;
    }
    static Acceptor* gatewayListen(const std::string& host_, MsgHandler* msg_handler_)
    {
        Singleton<NetData>::instance().start();
        AcceptorLinux* ret = new AcceptorLinuxGate(&(Singleton<NetData>::instance().epoll),
                                                   msg_handler_,
                                                   (Singleton<NetData>::instance().tg));

        if (ret->open(host_))
        {
            delete ret;
            return NULL;
        }
        Singleton<NetData>::instance().allAcceptor.push_back(ret);
        return ret;
    }
	static Acceptor* gatewayListen(ArgHelper& arg_helper, MsgHandler* msg_handler_)
    {
        Singleton<NetData>::instance().start();
        AcceptorLinuxGate* ret = new AcceptorLinuxGate(&(Singleton<NetData>::instance().epoll),
                                                   msg_handler_,
                                                   (Singleton<NetData>::instance().tg));

        if (ret->open(arg_helper))
        {
            delete ret;
            return NULL;
        }
        Singleton<NetData>::instance().allAcceptor.push_back(ret);
        return ret;
    }

    static SocketObjPtr connect(const std::string& host_, MsgHandler* msg_handler_)
    {
        Singleton<NetData>::instance().start();
        return Connector::connect(host_, &(Singleton<NetData>::instance().epoll), msg_handler_,
                                    (Singleton<NetData>::instance().tg->rand_alloc()));
    }
};

}

#endif
