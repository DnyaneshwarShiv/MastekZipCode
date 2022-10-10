import { fork } from "redux-saga/effects";
import {autoCompletionSaga , getAreaDetailsWatcher}from "../postcode/postcode-saga";

export default function* rootSaga() {
    yield fork(autoCompletionSaga)
    yield fork(getAreaDetailsWatcher)
}