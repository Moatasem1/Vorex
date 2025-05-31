import { VolatilityLevel } from '../../application/cryptos/models/crypto.model';

export function roiTextClassColor(risk: number): string {
  if (risk < 30) return 'text-green-500';
  if (risk < 70) return 'text-yellow-500';
  return 'text-red-500';
}

export function roikBackgroundClassColor(risk: number): string {
  if (risk < 30) return 'bg-green-500';
  if (risk < 70) return 'bg-yellow-500';
  return 'bg-red-500';
}

export function getVoltiltlyLevel(voltiltlyLevelId: VolatilityLevel): string {
  switch (voltiltlyLevelId) {
    case VolatilityLevel.Low:
      return 'Low';
    case VolatilityLevel.Medium:
      return 'Medium';
    case VolatilityLevel.High:
      return 'High';
  }
}

export function getVoltiltlyLevelColor(
  voltiltlyLevelId: VolatilityLevel
): string {
  switch (voltiltlyLevelId) {
    case VolatilityLevel.Low:
      return 'text-green-500';
    case VolatilityLevel.Medium:
      return 'text-yellow-500';
    case VolatilityLevel.High:
      return 'text-red-500';
  }
}
export function getVoltiltlyLevelBackgroundColor(
  voltiltlyLevelId: VolatilityLevel,
  opacity: number = 100
): string {
  switch (voltiltlyLevelId) {
    case VolatilityLevel.Low:
      return `bg-green-500`;
    case VolatilityLevel.Medium:
      return `bg-yellow-500`;
    case VolatilityLevel.High:
      return `bg-red-500`;
  }
}
