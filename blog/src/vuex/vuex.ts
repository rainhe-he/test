import {createStore} from 'vuex'

const store = createStore({
    //状态变量
    state(){
        return{
            Token: localStorage["Token"],
            Name: localStorage["Name"]
        }
    },
    //方法
    mutations:{
        SettingName(state : any, Name){
            state.Name = Name
        },
        SettingToken(state : any, Token){
            state.Token = Token
        }
    }

})
export default store
