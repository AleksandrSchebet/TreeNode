import { createSlice, PayloadAction } from '@reduxjs/toolkit'

interface initState {
    init: boolean
}

const initialState: initState = {
    init: true,
}

export const initSlice = createSlice({
    name: 'init',
    initialState,
    reducers: create => ({ 
        setInit: create.reducer((state, action: PayloadAction<boolean>) => {
            state.init = action.payload;
        }),
    }),
    selectors: {
        isInit: state => state.init,
    },
})

export const { setInit } = initSlice.actions

export const { isInit } = initSlice.selectors

export default initSlice.reducer