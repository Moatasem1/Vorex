import { Observable } from 'rxjs';

export interface UseCase<S, T> {
  execute(input: S): Observable<T>;
}
